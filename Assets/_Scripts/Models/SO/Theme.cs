using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Theme Asset")]
    public class Theme : ScriptableObject
    {
        [ReadOnly]
        public int ID;
        [ReadOnly]
        public LevelBtn levelBtnPrefab;
        [ReadOnly]
        public Sprite levelGround;
        [ReadOnly]
        public List<Vector3> btnPositions;
        [ReadOnly]
        public Sprite lockBtn;
        [ReadOnly]
        public Sprite unlockBtn;
        [ReadOnly]
        public Sprite playedBtn;
    }
}
