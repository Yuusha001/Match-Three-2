using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Level Difficulty")]
    [System.Serializable]
    public class LevelDifficulty : ScriptableObject
    {
        public MapData mapData;

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

        [Button("Get All Tiles")]
        private void GetAllTiles()
        {
            tileTypes = Resources.LoadAll<TileTypeAsset>("_SO/TileTypes");
        }
        private void OnValidate()
        {
            mapData.InitializeMapData();
        }
    }

    [System.Serializable]
    public class MapData
    {
        [Header("Level Difficulty")]
        [Range(6, 8)]
        public int numberOfRows = 6;
        [Range(6, 8)]
        public int numberOfCols = 6;
        public EMapType[,] Data;
        public void InitializeMapData()
        {
            Data = new EMapType[numberOfRows, numberOfCols];
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
        Normal = 0, Empty = 1, Block_1 = 2, Block_2 = 3, Rock_1 = 4, Rock_2= 5, Frozen = 6
    }
}

