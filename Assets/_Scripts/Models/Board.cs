using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace MatchThree
{
    public class Board : MonoBehaviour
    {
        [Header("Setup")]
        [ReadOnly]
        public Tile tilePrefab;
        [ReadOnly]
        public Row rowPrefab;
        [ReadOnly]
        [SerializeField]
        private Transform holder;
        [ReadOnly]
        [SerializeField]
        private CharacterGoal characterGoal;
        [ReadOnly]
        public Row[] rows;
        [ReadOnly]
        [SerializeField]
        private TileTypeAsset[] tileTypes;
        [ReadOnly]
        [SerializeField]
        private Transform swappingOverlay;
        [SerializeField] 
        private float tweenDuration = 0.25f;

        [ReadOnly]
        [SerializeField]
        private bool isSwapping;
        [ReadOnly]
        [SerializeField]
        private bool isMatching;
        [ReadOnly]
        [SerializeField]
        private bool isShuffling;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly List<Tile> _selection = new List<Tile>();
        private TileData[,] Matrix
        {
            get
            {
                var width = rows.Max(row => row.tiles.Length);
                var height = rows.Length;

                var data = new TileData[width, height];

                for (var y = 0; y < height; y++)
                    for (var x = 0; x < width; x++)
                        data[x, y] = GetTile(x, y).Data;

                return data;
            }
        }

        private Tile GetTile(int x, int y)
        {
            return rows[y].tiles[x];
        }
        private Tile[] GetTiles(IList<TileData> tileData)
        {
            var tiles = new Tile[tileData.Count];

            for (var i = 0; i < tileData.Count; i++) 
                tiles[i] = GetTile(tileData[i].X, tileData[i].Y);

            return tiles;
        }

        public void Initialize(LevelDifficulty levelDifficulty)
        {
            SettupBoard(levelDifficulty);
            _ = Delay.DoAction(() =>
            {
                while (TileDataMatrixUtility.FindBestMatch(Matrix) != null)
                {
                    Shuffle();
                }
            }, 0);
            
        }



        private void SettupBoard(LevelDifficulty levelDifficulty)
        {
            //Clean
            DeInitialize();

            GameManager.OnMatch += MatchingHandler;
            //Setup Board
            tileTypes = levelDifficulty.tileTypes;
            rows = new Row[levelDifficulty.numberOfRows];
            for (int i = 0; i < levelDifficulty.numberOfCols; i++)
            {
                var row = Instantiate(rowPrefab, holder);
                row.Initialize(this, levelDifficulty.numberOfCols, i, tileTypes);
                rows[i] = row;
            }
            characterGoal.Initialize(levelDifficulty);
        }

        private void MatchingHandler(TileTypeAsset type, int count)
        {
            LogManager.Instance.Log($"Matched {count} x {type.name}.", this);
            GameManager.Matching(type,count);
        }

        [Button("Clear")]
        public void DeInitialize()
        {
            if (rows != null && rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    Destroy(rows[i].gameObject);
                }
            }
            rows = null;
            GameManager.OnMatch -= MatchingHandler;
            characterGoal.DeInitialize();
        }

        public async void Swipe(Tile tile, SwipeDir swipeDir)
        {
            switch (swipeDir)
            {
                case SwipeDir.Left:
                    await MoveAsync(tile, GetTile(tile.x - 1, tile.y));
                    break;
                case SwipeDir.Right:
                    await MoveAsync(tile, GetTile(tile.x + 1, tile.y));
                    break;
                case SwipeDir.Up:
                    await MoveAsync(tile, GetTile(tile.x, tile.y - 1));
                    break;
                case SwipeDir.Down:
                    await MoveAsync(tile, GetTile(tile.x, tile.y + 1));
                    break;
                default:
                    break;
            }
        }

        public async void Select(Tile tile)
        {
            Debug.Log(tile.x + " " + tile.y);
            if (isSwapping || isMatching || isShuffling) return;

            if (!_selection.Contains(tile))
            {
                if (_selection.Count > 0)
                {
                    if (Math.Abs(tile.x - _selection[0].x) == 1 && Math.Abs(tile.y - _selection[0].y) == 0
                        || Math.Abs(tile.y - _selection[0].y) == 1 && Math.Abs(tile.x - _selection[0].x) == 0)
                        _selection.Add(tile);
                }
                else
                {
                    _selection.Add(tile);
                }
            }

            if (_selection.Count < 2) return;
            await MoveAsync(_selection[0], _selection[1]);
            _selection.Clear();
        }

        private async UniTask MoveAsync(Tile tile1, Tile tile2)
        {
            await SwapAsync(tile1, tile2);
            GameManager.ValidMoving();
            if (!await MatchAsync())
            {
                await SwapAsync(tile1, tile2);
            }

            var matrix = Matrix;

            while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
            {
                Shuffle();
                matrix = Matrix;
            }
        }


        private Sequence DeflateSequence(Tile[] tiles)
        {
            var Sequence = DOTween.Sequence();
            for (int i = 0; i < tiles.Length; i++)
            {
                Sequence.Join(tiles[i].deflate(tweenDuration));
            }
            return Sequence;
        }

        private Sequence InflateSequence(Tile[] tiles)
        {
            var Sequence = DOTween.Sequence();
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = tiles[i].y; j >= 0; j--)
                {
                    if (j > 0)
                        GetTile(tiles[i].x, j).Type = GetTile(tiles[i].x, j - 1).Type;
                    if (j == 0)
                        GetTile(tiles[i].x, j).Type = tileTypes[Random.Range(0, tileTypes.Length)];
                }
                Sequence.Join(tiles[i].inflate(tweenDuration));
            }
            return Sequence;
        }

        private async UniTask<bool> MatchAsync()
        {
            var didMatch = false;

            isMatching = true;

            var match = TileDataMatrixUtility.FindBestMatch(Matrix);

            while (match != null)
            {
                didMatch = true;

                var tiles = GetTiles(match.Tiles);

                await DeflateSequence(tiles).Play().ToUniTask();

                AudioManager.Instance.sfx.PlayOneShot(AudioManager.Instance.matchSound);

                await InflateSequence(tiles).Play().ToUniTask();

                GameManager.Matching(Array.Find(tileTypes, tileType => tileType.id == match.TypeId), match.Tiles.Length);

                match = TileDataMatrixUtility.FindBestMatch(Matrix);
            }

            isMatching = false;

            return didMatch;
        }

        private Sequence SwapSequence(Transform icon1Transform, Transform icon2Transform)
        {
            var Sequence = DOTween.Sequence();
            Sequence.Join(icon1Transform.DOMove(icon2Transform.position, tweenDuration).SetEase(Ease.OutBack));
            Sequence.Join(icon2Transform.DOMove(icon1Transform.position, tweenDuration).SetEase(Ease.OutBack));
            return Sequence;
        }

        private async UniTask SwapAsync(Tile tile1, Tile tile2)
        {
             isSwapping = true;

            var icon1 = tile1.icon;
            var icon2 = tile2.icon;

            var icon1Transform = icon1.transform;
            var icon2Transform = icon2.transform;

            icon1Transform.SetParent(swappingOverlay);
            icon2Transform.SetParent(swappingOverlay);

            icon1Transform.SetAsLastSibling();
            icon2Transform.SetAsLastSibling();

            await SwapSequence(icon1Transform, icon2Transform).Play().ToUniTask();

            icon1Transform.SetParent(tile2.transform);
            icon2Transform.SetParent(tile1.transform);

            tile1.icon = icon2;
            tile2.icon = icon1;

            var tile1Item = tile1.Type;

            tile1.Type = tile2.Type;

            tile2.Type = tile1Item;

            isSwapping = false;
        }

        private void Shuffle()
        {
            isShuffling = true;
            
            foreach (var row in rows)
                foreach (var tile in row.tiles)
                    tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];

            isShuffling = false;
        }
    }
}
