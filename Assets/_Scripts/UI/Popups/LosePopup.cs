using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class LosePopup : PopupUI
    {
        public StarIcon[] starIcons;
        public TextMeshProUGUI scoreTxt;
        public Button menuBtn;
        public Button replayBtn;
        public Button quitBtn;
        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            replayBtn.onClick.AddListener(ReplayHander);
            quitBtn.onClick.AddListener(QuitHandler);
            menuBtn.onClick.AddListener(MenuHandler);
        }

        private void MenuHandler()
        {
            throw new NotImplementedException();
        }

        private void QuitHandler()
        {
            throw new NotImplementedException();
        }

        private void ReplayHander()
        {
            throw new NotImplementedException();
        }
    }
}
