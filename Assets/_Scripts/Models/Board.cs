using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MatchThree
{
    public class Board : MonoBehaviour
    {
        public Tile tilePrefab;
        public Row rowPrefab;
        [SerializeField]
        private Transform holder;

        public Row[] rows;
        [SerializeField] private TileTypeAsset[] tileTypes;

        [Button("Test")]
        public void Initialize()
        {   
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    Destroy(rows[i].gameObject);
                }
            }

            //Setup Board
            rows = new Row[7];
            for (int i = 0; i < 7; i++)
            {
                var row = Instantiate(rowPrefab, holder);
                row.Initialize(this, 7,i,tileTypes);
                rows[i] = row;
            }
           
        }

        [Button("Clear")]
        public void Clear()
        {
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    DestroyImmediate(rows[i].gameObject);
                }
            }
            rows = null;
        }


        public async void Select(Tile tile)
        {
            Debug.Log(tile.x + " " + tile.y);
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
