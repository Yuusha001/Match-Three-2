using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Level Difficulty")]
    [System.Serializable]
    public class LevelDifficulty : ScriptableObject
    {
        [Header("Level Difficulty")]
        [Range(6, 9)]
        public int numberOfRows = 6;
        [Range(6, 9)]
        public int numberOfCols = 6;
        public EMapType squareType;
        [HideInInspector]
        public SquareBlocks[] Data;

        [HorizontalLine(color: EColor.Red)]
        [Space]
        [Header("Level Design")]
        public int numberOfMoves;
        public int[] starsRequiredPoints = new int[3];
        public TileTypeAsset[] tileTypes;
        public EGameType gameType;

        [HorizontalLine(color: EColor.Green)]
        [ShowIf("gameType", EGameType.Collect)]
        public CollectType[] collectTypes;
        public void GetAllTiles()
        {
            tileTypes = Resources.LoadAll<TileTypeAsset>("_SO/TileTypes");
        }

        public void Initialize()
        {
            Data = new SquareBlocks[numberOfCols * numberOfRows];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new SquareBlocks();
                Data[i].Initialize();
            }
        }
    }
    [System.Serializable]
    public struct SquareBlocks
    {
        public EMapType block;
        public EMapType obstacle;

        public void Initialize()
        {
            this.block = EMapType.Empty;
            this.obstacle = EMapType.Normal;
        }
    }

    [System.Serializable]
    public struct CollectType
    {
        public int numberOfCollects;
        public TileTypeAsset tileType;
    }

    public enum EGameType
    {
        None = 0,Collect = 1
    }
    public enum EMapType
    {
        Normal = 0, Empty = 1, Block_1 = 2, Block_2 = 3, Rock_1 = 4, Rock_2= 5, Frozen = 6, Thriving = 7
    }
}

