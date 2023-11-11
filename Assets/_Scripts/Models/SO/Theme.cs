using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Theme Asset")]
    public class Theme : ScriptableObject
    {
        [Header("Sprites")]
        public Sprite[] decoSprites;
        public Sprite[] groundSprites;

        [Header("Position")]
        public LevelBtn levelBtnPrefab;
        public List<Vector3> levelBtnPositions;
        public List<Vector3> levelBtnPositions2;
    }
}
