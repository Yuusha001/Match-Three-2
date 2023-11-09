using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;
using System;

namespace MatchThree
{
    public class SettingPopup : PopupUI
    {

        [SerializeField]
        private TextMeshProUGUI titleTxt;
        [SerializeField]
        private ToggleBtn musicBtn;
        [SerializeField]
        private ToggleBtn sfxBtn;
        [SerializeField]
        private Button replayBtn;
        [SerializeField]
        private Button quitBtn;
        [SerializeField]
        private Button closeBtn;
        [SerializeField]
        private Button creditBtn;
        public override void Initialize(PopupManager popupManager, System.Action onClosed = null)
        {
            base.Initialize(popupManager, CloseHandler);
            musicBtn.Initialize(MusicHandler);
            sfxBtn.Initialize(SFXHandler);
            replayBtn.onClick.AddListener(ReplayHander);
            quitBtn.onClick.AddListener(QuitHandler);
            closeBtn.onClick.AddListener(Close);
            creditBtn.onClick.AddListener(CreditHandler);
            titleTxt.text = "Setting";
        }

        

        public void Show(bool inGame = false)
        {
            titleTxt.text = inGame ? "Pause" : "Setting";
            Show();
        }

        private void CloseHandler()
        {
            titleTxt.text = "Setting";
        }

        private void MusicHandler()
        {
            if (musicBtn == null) return;
            LogManager.Instance.Log("Music Toggle",this);

        }

        private void SFXHandler()
        {
            if (sfxBtn == null) return;
            LogManager.Instance.Log("SFX Toggle",this);

        }

        private void ReplayHander()
        {
            LogManager.Instance.Log("Replay Handler",this);
        }

        private void QuitHandler()
        {
            LogManager.Instance.Log("Quit Handler", this);

        }

        private void CreditHandler()
        {
            throw new NotImplementedException();
        }
    }
}
