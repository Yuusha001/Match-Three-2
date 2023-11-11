using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Tile Type Asset")]
    public sealed class TileTypeAsset : ScriptableObject
    {
        public int id;

        public int value = 100;

        public Sprite sprite;
    }
}