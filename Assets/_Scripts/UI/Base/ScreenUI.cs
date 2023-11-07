using UnityEngine;

namespace MatchThree
{
    public abstract class ScreenUI : MonoBehaviour
    {
        protected UIManager _uiManager;
        public virtual void Initialize(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        public virtual void Active()
        {
            gameObject.SetActive(true);
        }
        public virtual void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}