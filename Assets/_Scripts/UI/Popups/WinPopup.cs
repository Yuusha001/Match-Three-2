using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Utils;

namespace MatchThree
{
    public class WinPopups : PopupUI
    {
        public StarIcon[] starIcons;
        public TextMeshProUGUI scoreTxt;
        public Button nextBtn;
        public Button replayBtn;
        public Button quitBtn;

        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            replayBtn.onClick.AddListener(ReplayHander);
            quitBtn.onClick.AddListener(QuitHandler);
            nextBtn.onClick.AddListener(NextHandler);
        }

        public override void Show()
        {
            base.Show();
            foreach (var item in starIcons)
            {
                item.Show(GameManager.Instance.currentPoint);
            }
        }

        private void NextHandler()
        {
            LogManager.Instance.Log("Next Handler", this);

        }

        private void QuitHandler()
        {
            LogManager.Instance.Log("Quit Handler", this);

        }

        private void ReplayHander()
        {
            LogManager.Instance.Log("Replay Handler", this);

        }
    }
}
