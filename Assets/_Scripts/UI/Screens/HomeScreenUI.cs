using System;
using UnityEngine.UI;
using UnityEngine;

namespace MatchThree
{
    public class HomeScreenUI : ScreenUI
    {
        [SerializeField]
        private Button playBtn;
        [SerializeField]
        private Button settingBtn;
        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            playBtn.onClick.AddListener(PlayHandler);
            settingBtn.onClick.AddListener(SettingHandler);
        }

        private void SettingHandler()
        {
            PopupManager.Instance.GetPopup<SettingPopup>().Show();
        }

        private void PlayHandler()
        {
            Deactive();
            UIManager.Instance.ActiveScreen<MenuScreenUI>();
        }
    }
}