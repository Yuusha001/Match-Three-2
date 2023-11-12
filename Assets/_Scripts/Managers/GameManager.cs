using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class GameManager : Singleton<GameManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] 
        private static void Initialize() => Application.targetFrameRate = 60;
        public int currentScore { get; private set; }
        public int currentValidMove { get; private set; }
        public LevelDifficulty currentLevelDifficulty { get; private set; }
        public static event Action<int> OnAddScore;
        public static event Action OnValidMove;
        public static event Action<TileTypeAsset, int> OnMatch;
        public static void AddScore(int score)
        {
            Instance.currentScore += score;
            OnAddScore?.Invoke(score);
        }

        public static void ValidMoving()
        {
            Instance.currentValidMove--;
            OnValidMove?.Invoke();
        }

        public static void Matching(TileTypeAsset tileType, int count)
        {
            OnMatch?.Invoke(tileType, count);
        }
    }
}