using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    public class StarIcon : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private Transform starVisual;
        [SerializeField]
        private Animator shineAnimator;
        [ReadOnly]
        [SerializeField]
        private int point;
        [ReadOnly]
        [SerializeField]
        private bool isShow;
        
        public void Initialize(int requiredPoint)
        {
            point = requiredPoint;
            isShow = false;
            starVisual.localScale = Vector3.zero;
        }


        public void Show(int currentPoint)
        {
            if (currentPoint>point && !isShow)
            {
                starVisual.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                     isShow = true;
                    shineAnimator.Play("Star Shine");
                });
            }
        }

        public void DeInitialize()
        {
            point = 0;
            isShow = false;
            starVisual.localScale = Vector3.one;
        }
    }
}
