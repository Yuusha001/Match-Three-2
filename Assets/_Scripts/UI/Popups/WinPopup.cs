using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Utils;
using NaughtyAttributes;

namespace MatchThree
{
    public class WinPopups : PopupUI
    {
        [ReadOnly]
        [SerializeField]
        private StarIcon[] starIcons;
        [ReadOnly]
        [SerializeField]
        private TextMeshProUGUI scoreTxt;
        [ReadOnly]
        [SerializeField]
        private Button nextBtn;
        [ReadOnly]
        [SerializeField]
        private Button replayBtn;

        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            replayBtn.onClick.AddListener(ReplayHander);
            nextBtn.onClick.AddListener(NextHandler);
            GameManager.OnStartGame += StartGame;
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
            Close();
            GameManager.Instance.QuitGame();
        }

        private void ReplayHander()
        {
            Close();
            GameManager.Instance.ReloadLevel();
        }

        private void OnDestroy()
        {
            GameManager.OnStartGame -= StartGame;
        }
    }
}
