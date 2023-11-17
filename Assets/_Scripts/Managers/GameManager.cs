using Cysharp.Threading.Tasks;
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


        public void StartGame(LevelDifficulty levelDifficulty)
        {
            currentScore = 0;
            currentLevelDifficulty = levelDifficulty;
            currentValidMove = levelDifficulty.numberOfMoves;
            AudioManager.Instance.PlayMusic(1, true);
            UIManager.Instance.GetScreen<IngameScreenUI>().StartGame();
        }


        private void AddScore(int score)
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
            int score = tileType.value * count;
            Instance.CheckingCollected(tileType, count);
            Instance.AddScore(score);
            OnMatch?.Invoke(tileType, count);
        }

        private bool CollectCompleted(CollectType[] types)
        {
            var sum = 0;
            foreach (var item in types)
            {
                sum += item.numberOfCollects;
            }
            return sum == 0;
        }

        private void CheckingCollected(TileTypeAsset tileType, int count)
        {   
            var collectTypes = Instance.currentLevelDifficulty.collectTypes;
            for (int i = 0; i < collectTypes.Length; i++)
            {
                if (collectTypes[i].tileType == tileType)
                {
                    collectTypes[i].numberOfCollects -= count;
                    break;
                }
            } 
        }

        public void CheckingWinCondition()
        {
            var starsRequiredPoints = Instance.currentLevelDifficulty.starsRequiredPoints;
            switch (Instance.currentLevelDifficulty.gameType)
            {
                case EGameType.None:
                    if (currentScore >= starsRequiredPoints[starsRequiredPoints.Length - 1] )
                    {
                        PopupManager.Instance.ShowPopup<WinPopups>();
                        return;
                    }
                    if (currentValidMove == 0)
                    {
                        PopupManager.Instance.ShowPopup<LosePopup>();
                        return;
                    }
                    break;  
                case EGameType.Collect:
                    var collectTypes = Instance.currentLevelDifficulty.collectTypes;
                    if (CollectCompleted(collectTypes))
                    {
                        if (currentScore > starsRequiredPoints[starsRequiredPoints.Length - 1] || currentValidMove == 0)
                        {
                            PopupManager.Instance.ShowPopup<WinPopups>();
                        }
                        return;
                    }
                    if (currentValidMove == 0)
                    {
                        PopupManager.Instance.ShowPopup<LosePopup>();
                        return;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}