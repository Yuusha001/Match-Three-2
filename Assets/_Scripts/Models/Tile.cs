using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MatchThree
{
    public sealed class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image icon;
        [SerializeField]
        private Button button;
        private Board _board; 
        public int x { get; private set; }
        public int y { get; private set; }
        public TileData Data => new TileData(x, y, _type.id);
        private TileTypeAsset _type;
        public TileTypeAsset Type
        {
            get => _type;

            set
            {
                if (_type == value) return;

                _type = value;

                icon.sprite = _type.sprite;
            }
        }

        [Header("Swipe")]
        [SerializeField]
        private float swipeAngle = 0;
        [SerializeField]
        private float swipeResist = 1f;
        [SerializeField]
        private Vector2 firstTouchPosition;
        [SerializeField]
        private Vector2 finalTouchPosition;
        public void Initialize(Board board, Action action, int _x, int _y, TileTypeAsset[] tileTypes)
        {
            _board = board;
            x = _x;
            y = _y;
            button.onClick.AddListener(()=>action());
            Type = tileTypes[Random.Range(0, tileTypes.Length)];
            action?.Invoke();
        }

        private void CalculateAngle()
        {
            if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
            {
                swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
                if (swipeAngle > -45 && swipeAngle <= 45 && x < _board.rows[y].tiles.Length - 1)
                {
                    //Right Swipe
                    Debug.Log($"[{x},{y}] Right");
                    //_board.Select2(this, _board.GetTile(x + 1, y));

                }
                else if (swipeAngle > 45 && swipeAngle <= 135 && y < _board.rows.Length - 1)
                {
                    //Up Swipe
                    Debug.Log($"[{x},{y}] Up");
                    //board.Select2(this, board.GetTile(x, y - 1));

                }
                else if ((swipeAngle > 135 || swipeAngle <= -135) && x > 0)
                {
                    //Left Swipe
                    Debug.Log($"[{x},{y}] Left");
                    //board.Select2(this, board.GetTile(x - 1, y));

                }
                else if (swipeAngle < -45 && swipeAngle >= -135 && y > 0)
                {
                    //Down Swipe
                    Debug.Log($"[{x},{y}] Down");
                    //board.Select2(this, board.GetTile(x, y + 1));

                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }
}