using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class LevelDifficulty : ScriptableObject
    {
        [Header("Level Difficulty")]
        [Range(7,8)]
        public int numberOfRows;
        [Range(7, 8)]
        public int numberOfCols;
        [Space]
        [Header("Level Design")]
        public int numberOfMoves;
    }
}
