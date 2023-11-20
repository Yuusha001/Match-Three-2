using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class ScoreBar : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        private StarIcon[] starIcons;
        [SerializeField]
        [ReadOnly]
        private Slider scoreSlider;
        private int levelPassedScore;
        public void Initialize(LevelDifficulty levelDifficulty)
        {
            var passedPoints = levelDifficulty.starsRequiredPoints;
            for (int i = 0; i < starIcons.Length; i++)
            {
                starIcons[i].Initialize(passedPoints[i]);
            }
            levelPassedScore = passedPoints[passedPoints.Length - 1];
            scoreSlider.value = 0;
            GameManager.OnAddScore += AddScoreHandler;
        }

        private void AddScoreHandler(int obj)
        {
            var currentScore = GameManager.Instance.currentScore;   
            foreach (var item in starIcons)
            {
                item.Show(currentScore);
            }
            var targetValue = currentScore > levelPassedScore ? 1 : (float)currentScore / levelPassedScore;
            scoreSlider.value = targetValue;

        }

        public void DeInitialize()
        {
            GameManager.OnAddScore -= AddScoreHandler;
            levelPassedScore = 0;
            scoreSlider.value = 0;
            foreach (var item in starIcons)
            {
                item.DeInitialize();
            }
        }
    }
}
