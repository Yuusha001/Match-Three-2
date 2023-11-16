using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchThree
{
    public class LevelManager : Singleton<LevelManager>
    {
        [ReadOnly]
        [SerializeField]
        private List<LevelDifficulty> _levels;

        [Button("Load All Levels")]
        private void LoadLevels()
        {
            _levels.Clear();
            _levels = Resources.LoadAll<LevelDifficulty>("_SO/LevelDifficulties").ToList<LevelDifficulty>();
        }

        public void LoadLevel(int curentLevel)
        {
            if (curentLevel > _levels.Count || curentLevel < 0) return;
            GameManager.Instance.StartGame(_levels[curentLevel]);
        }
    }
}
