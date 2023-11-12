using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    public class IngameScreenUI : ScreenUI
    {
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI moveTxt;
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI scoreTxt;
        [ReadOnly]
        [SerializeField]
        private UnityEngine.UI.Button settingBtn;
        [ReadOnly]
        [SerializeField]
        private ScoreBar scoreBar;
        [ReadOnly]
        [SerializeField]
        private Board boardGame;

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            settingBtn.onClick.AddListener(SettingHandler);
            boardGame.Initialize(GameManager.Instance.currentLevelDifficulty);
            scoreBar.Initialize(GameManager.Instance.currentLevelDifficulty);
            GameManager.OnAddScore += AddScoreHandler;
            GameManager.OnValidMove += ValidMoveHandler;
        }

        public override void Active()
        {
            base.Active();
            moveTxt.text = GameManager.Instance.currentValidMove.ToString();
            scoreTxt.text = GameManager.Instance.currentScore.ToString();
        }

        public override void Deactive()
        {
            base.Deactive();
            moveTxt.text = "";
            scoreTxt.text = "";
            GameManager.OnAddScore -= AddScoreHandler;
            GameManager.OnValidMove -= ValidMoveHandler;
            scoreBar.DeInitialize();
            boardGame.DeInitialize();
        }

        private void ValidMoveHandler()
        {
            moveTxt.text = GameManager.Instance.currentValidMove.ToString();
        }

        private void AddScoreHandler(int obj)
        {
            scoreTxt.text = GameManager.Instance.currentScore.ToString();
        }

        private void SettingHandler()
        {
            PopupManager.Instance.GetPopup<SettingPopup>().Show(true);
        }
    }
}