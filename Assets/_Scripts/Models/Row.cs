using UnityEngine;

namespace MatchThree
{
    public sealed class Row : MonoBehaviour
    {
        public Tile[] tiles;
        public void Initialize(Board board, int numberOfTiles, int y, TileTypeAsset[] tileTypes)
        {
            tiles = new Tile[numberOfTiles];
            for (int x = 0; x < numberOfTiles; x++)
            {
                tiles[x] = Instantiate(board.tilePrefab, this.transform);
                tiles[x].name = $"[{x},{y}]";
                tiles[x].Initialize(board, x, y, tileTypes);
            }
        }
    }
}
