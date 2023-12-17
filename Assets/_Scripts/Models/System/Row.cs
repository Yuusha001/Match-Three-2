using PathologicalGames;
using UnityEngine;
using Utils;

namespace MatchThree
{
    public sealed class Row : MonoBehaviour
    {
        public Tile[] tiles;
        public void Initialize(Board board, int numberOfTiles, int x, SquareBlocks[] data)
        {
            tiles = new Tile[numberOfTiles];
            for (int y = 0; y < numberOfTiles; y++)
            {
                tiles[y] = FactoryObject.Spawn<Tile>(StringManager.BoardPool, StringManager.TilePrefab,this.transform);
                tiles[y].name = $"[{x},{y}]";
                tiles[y].Initialize(board, x, y, data[x*numberOfTiles + y] );
            }
        }

        public void DeInitialize()
        {
            if (tiles != null && tiles.Length > 0)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].DeInitialize();
                    FactoryObject.Despawn(StringManager.BoardPool, tiles[i].transform, PoolManager.Pools[StringManager.BoardPool].transform);
                }
            }
        }

        public void BorderInitialize()
        {
            if (tiles != null && tiles.Length > 0)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                     tiles[i].BorderInitialize();
                }
            }
        }
    }
}
