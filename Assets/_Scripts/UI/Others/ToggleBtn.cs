using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class ToggleBtn : MonoBehaviour
    {
        protected bool onEnable;
        protected System.Action onToggle;
        public RectTransform btnTranform;
        private Vector3 _onPosition = new Vector3(85,0,0);
        private Vector3 _offPosition = new Vector3(-85, 0, 0);

        public virtual void Initialize(bool _onEnable, System.Action _onToggle = null)
        {
            onToggle = _onToggle;
            onEnable = _onEnable;
            btnTranform.anchoredPosition = onEnable ? _onPosition : _offPosition;
            btnTranform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Toggle);
        }
        public virtual void Toggle()
        {
            if (onToggle != null)
                onToggle.Invoke();
            onEnable = !onEnable;
            btnTranform.DOLocalMove(onEnable ? _onPosition : _offPosition, 0.5f);

        }
    }
}
