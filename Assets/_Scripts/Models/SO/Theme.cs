using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Theme Asset")]
    public class Theme : ScriptableObject
    {
        [Header("Sprites")]
        [ReadOnly]
        public Sprite[] decoSprites;
        [ReadOnly]
        public Sprite[] groundSprites;

        [Header("Position")]
        [ReadOnly]
        public LevelBtn levelBtnPrefab;
        [ReadOnly]
        public List<Vector3> levelBtnPositions;
        [ReadOnly]
        public List<Vector3> levelBtnPositions2;
    }
}
