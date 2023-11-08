using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class StarIcon : MonoBehaviour
    {
        [SerializeField]
        private Transform starVisual;

        [SerializeField]
        private int point;

        public void Initialize(int requiredPoint)
        {
            point = requiredPoint;
        }


        public void Show(int currentPoint)
        {
            if(currentPoint>point)
                starVisual.DOScale(Vector3.one, 0.4f);
        }
    }
}
