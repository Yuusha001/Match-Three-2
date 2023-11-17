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
        [Header("Level Difficulty")]
        [Range(6,8)]
        public int numberOfRows = 6;
        [Range(6, 8)]
        public int numberOfCols = 6;
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
    }

    [System.Serializable]
    public struct CollectType
    {
        public int numberOfCollects;
        public TileTypeAsset tileType;

    }

    public enum EGameType
    {
        None,Collect
    }
}

