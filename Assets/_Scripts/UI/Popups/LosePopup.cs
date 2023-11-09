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
        [SerializeField]
        private StarIcon[] starIcons;
        [SerializeField]
        private TextMeshProUGUI scoreTxt;
        [SerializeField]
        private Button menuBtn;
        [SerializeField]
        private Button replayBtn;
        [SerializeField]
        private Button quitBtn;
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
