using UnityEngine;

namespace MatchThree
{
    public sealed class Row : MonoBehaviour
    {
        public Tile[] tiles;
        public void Initialize(Board board, int numberOfTiles, int x, TileTypeAsset[] tileTypes)
        {
            tiles = new Tile[numberOfTiles];
            for (int y = 0; y < numberOfTiles; y++)
            {
                tiles[y] = Instantiate(board.tilePrefab, this.transform);
                tiles[y].name = $"[{x},{y}]";
                tiles[y].Initialize(board, () => board.Select(tiles[y]), x, y, tileTypes);
            }
        }
    }
}
