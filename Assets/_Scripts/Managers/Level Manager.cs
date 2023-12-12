using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchThree
{
    public class LevelManager : Singleton<LevelManager>
    {
        [ReadOnly]
        [SerializeField]
        private List<LevelDifficulty> levels;
        [ReadOnly]
        [SerializeField]
        private int currentLevel;

        [Button("Load All Levels")]
        private void LoadLevels()
        {
            levels.Clear();
            levels = Resources.LoadAll<LevelDifficulty>("_SO/LevelDifficulties").ToList<LevelDifficulty>();
        }

        private void Start()
        {
            DataManager.Instance.userData.LoadData(levels.Count);
        }

        public void LoadLevel(int _currentLevel)
        {
            if (_currentLevel > levels.Count || _currentLevel < 0) return;
            GameManager.Instance.StartGame(levels[_currentLevel]);
            currentLevel = _currentLevel;
        }

        public void ReloadLevel()
        {
            LoadLevel(currentLevel);
        }

        public void SaveLevelData()
        {
            var LV = DataManager.Instance.userData.userLevelDatas.Find(c => c.id == currentLevel);
            var score = GameManager.Instance.currentScore;
            var starObtained = GameManager.Instance.StarObtained();
            LV.Save(starObtained, score);
            DataManager.Instance.userData.SaveUserData();
        }
        public void UnlockNextLV()
        {
            var currentLV = DataManager.Instance.userData.GetUserLevelData(currentLevel);
            var nextLV = DataManager.Instance.userData.GetUserLevelData(currentLevel+1);
            currentLV.PlayedLevel();
            if (!nextLV.isUnlocked())
            {
                nextLV.UnlockLevel();
            }
            DataManager.Instance.userData.SaveUserData();
        }
    }
}
