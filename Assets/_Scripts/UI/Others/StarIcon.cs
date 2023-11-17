using Cysharp.Threading.Tasks;
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
