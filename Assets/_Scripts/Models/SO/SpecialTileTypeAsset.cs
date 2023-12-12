using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    [CreateAssetMenu(menuName = "Match Three/Special Tile Type Asset")]
    public sealed class SpecialTileTypeAsset : ScriptableObject
    {
        [ReadOnly]
        public Sprite sprite;
        [ReadOnly]
        public ESpecialType specialType;
    }

    public enum ESpecialType
    {
        None = 0, Horizontal = 1, Vertical = 2, Boom = 3, Lightning = 4
    }
}
