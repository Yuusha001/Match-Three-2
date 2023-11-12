using DG.Tweening;
using UnityEngine;

namespace MatchThree
{
    public class StarIcon : MonoBehaviour
    {
        [SerializeField]
        private Transform starVisual;

        [SerializeField]
        private int point;
        private bool isShow;
        public void Initialize(int requiredPoint)
        {
            point = requiredPoint;
            isShow = false;
        }


        public void Show(int currentPoint)
        {
            if(currentPoint>point && !isShow)
            {
                isShow = true;
                starVisual.DOScale(Vector3.one, 0.4f);
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
