using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Utils;

namespace MatchThree
{
    public class WinPopups : PopupUI
    {
        [SerializeField]
        private StarIcon[] starIcons;
        [SerializeField]
        private TextMeshProUGUI scoreTxt;
        [SerializeField]
        private Button nextBtn;
        [SerializeField]
        private Button replayBtn;
        [SerializeField]
        private Button quitBtn;

        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            replayBtn.onClick.AddListener(ReplayHander);
            quitBtn.onClick.AddListener(QuitHandler);
            nextBtn.onClick.AddListener(NextHandler);
            var passedPoints = GameManager.Instance.currentLevelDifficulty.starsRequiredPoints;
            for (int i = 0; i < starIcons.Length; i++)
            {
                starIcons[i].Initialize(passedPoints[i]);
            }
        }

        public override void Show()
        {
            base.Show();
            foreach (var item in starIcons)
            {
                item.Show(GameManager.Instance.currentScore);
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
