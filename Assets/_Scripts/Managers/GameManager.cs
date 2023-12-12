using System;
using System.Linq;

namespace MatchThree
{
    public class GameManager : Singleton<GameManager>
    {
        public int currentScore { get; private set; }
        public int currentValidMove { get; private set; }
        public LevelDifficulty currentLevelDifficulty { get; private set; }
        public CollectType[] collects { get; private set; }
        public static event Action<TileTypeAsset, int> OnMatch;
        public static event Action<int> OnAddScore;
        public static event Action OnValidMove;
        public static event Action OnStartGame;
        public static event Action OnQuitGame;

        public int StarObtained()
        {
            int numberOfStars = 0;
            for (int i = 0; i < 3; i++)
            {
                if (currentScore > currentLevelDifficulty.starsRequiredPoints[i])
                {
                    numberOfStars++;
                }
            }
            return numberOfStars;
        }

        public void StartGame(LevelDifficulty levelDifficulty)
        {
            Reset();
            currentLevelDifficulty = levelDifficulty;
            currentValidMove = levelDifficulty.numberOfMoves;
            switch (levelDifficulty.gameType)
            {
                case EGameType.Collect:
                    collects = new CollectType[levelDifficulty.collectTypes.Length];
                    for (int i = 0; i < collects.Length; i++)
                    {
                        collects[i] = levelDifficulty.collectTypes[i];
                    }
                    break;
                default:
                    break;
            }
            OnStartGame?.Invoke();
            AudioManager.Instance.PlayMusic(1, true);
        }

        public void Reset()
        {
            currentScore = 0;
            currentLevelDifficulty = null;
            currentValidMove = 0;
            collects = null;
        }

        public void ReloadLevel()
        {
            Reset();
            LevelManager.Instance.ReloadLevel();
        }

        public void QuitGame(Action action = null)
        {
            Reset();
            OnQuitGame?.Invoke();
            if (action != null)
            {
                action?.Invoke();
            }
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
            return types.All(element => element.numberOfCollects <= 0);
        }

        private void CheckingCollected(TileTypeAsset tileType, int count)
        {   
            for (int i = 0; i < collects.Length; i++)
            {
                if (collects[i].tileType == tileType)
                {
                    collects[i].numberOfCollects -= count;
                    break;
                }
            } 
        }

        private void EndGame(bool isWin)
        {
            LevelManager.Instance.SaveLevelData();
            if (isWin)
            {
                PopupManager.Instance.ShowPopup<WinPopups>();
                LevelManager.Instance.UnlockNextLV();
            }
            else
            {
                PopupManager.Instance.ShowPopup<LosePopup>();
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
                        EndGame(true);
                        return;
                    }
                    if (currentValidMove == 0)
                    {
                        EndGame(false);
                        return;
                    }
                    break;  
                case EGameType.Collect:
                    if (CollectCompleted(collects))
                    {
                        if (currentScore > starsRequiredPoints[starsRequiredPoints.Length - 1] || currentValidMove == 0)
                        {
                            EndGame(true);
                        }
                        return;
                    }
                    if (currentValidMove == 0)
                    {
                        EndGame(false);
                        return;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}