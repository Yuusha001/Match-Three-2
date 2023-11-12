using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Tile Type Asset")]
    public sealed class TileTypeAsset : ScriptableObject
    {
        [ReadOnly]
        public int id;
        [ReadOnly]
        public int value = 100;
        [ReadOnly]
        public Sprite sprite;
    }
}