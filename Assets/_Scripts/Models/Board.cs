using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace MatchThree
{
    public class Board : MonoBehaviour
    {
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
        public void Initialize(LevelDifficulty levelDifficulty)
        {
            
            SettupBoard(levelDifficulty);
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

        public async void Select(Tile tile)
        {
            //Debug.Log(tile.x + " " + tile.y);
            /*if (_isSwapping || _isMatching || _isShuffling) return;

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
            OnMove?.Invoke();
            await SwapAsync(_selection[0], _selection[1]);

            if (!await TryMatchAsync()) await SwapAsync(_selection[0], _selection[1]);

            var matrix = Matrix;

            while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
            {
                Shuffle();
                matrix = Matrix;
            }
            gameplayManager.CheckWinCondition();
            _selection.Clear();*/
        }
    }
}
