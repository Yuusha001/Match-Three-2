using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class ToggleBtn : MonoBehaviour
    {
        protected bool _onEnable;
        protected System.Action _onToggle;
        public RectTransform btnTranform;
        private Vector3 _onPosition = new Vector3(85,0,0);
        private Vector3 _offPosition = new Vector3(-85, 0, 0);

        public virtual void Initialize(System.Action onToggle = null)
        {
            _onToggle = onToggle;
            btnTranform.anchoredPosition = _onEnable ? _onPosition : _offPosition;
            btnTranform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Toggle);
        }
        public virtual void Toggle()
        {
            if (_onToggle != null)
                _onToggle();
            _onEnable = !_onEnable;
            btnTranform.DOLocalMove(_onEnable ? _onPosition : _offPosition, 0.5f);

        }
    }
}
