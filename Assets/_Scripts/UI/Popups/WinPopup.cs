using System;
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
            
        }

        public void StartGame()
        {
            var passedPoints = GameManager.Instance.currentLevelDifficulty.starsRequiredPoints;
            for (int i = 0; i < starIcons.Length; i++)
            {
                starIcons[i].Initialize(passedPoints[i]);
            }
        }

        public async void EndGameAsync(int currentScore)
        {
            scoreTxt.text = currentScore.ToString();
            for (int i = 0; i < starIcons.Length; i++)
            {
                await Delay.DoAction(() => starIcons[i].Show(currentScore), 0.35f * i);
            }
        }

        public override void Show()
        {
            base.Show();
            EndGameAsync(GameManager.Instance.currentScore);
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
