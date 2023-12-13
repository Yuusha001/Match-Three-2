using Cysharp.Threading.Tasks;
using DG.Tweening;
using PathologicalGames;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace MatchThree
{
    public sealed class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Image icon;
        public Image specialIcon;
        private Board _board;
        public int x { get; private set; }
        public int y { get; private set; }
        public TileData Data => new TileData(x, y, _type.id);
        private TileTypeAsset _type;
        private SpecialTileTypeAsset _specialType;
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

        public SpecialTileTypeAsset SpecialType
        {
            get => _specialType;

            set
            {
                if (_specialType == value) return;

                _specialType = value;
                specialIcon.sprite = (_specialType != null) ? _specialType.sprite:null;
                specialIcon.enabled = (SpecialType != null);
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
        public void Initialize(Board board, int _x, int _y, TileTypeAsset[] tileTypes)
        {
            _board = board;
            x = _x;
            y = _y;
            SpecialType = null;
            this.GetComponent<Image>().sprite = DataManager.Instance.boardSprites[(x + y) % 2 == 0 ? 0 : 1];
            Type = tileTypes[Random.Range(0, tileTypes.Length)];
            specialIcon.enabled = (SpecialType != null);

        }
        private void CalculateAngle()
        {
            if (Vector2.Distance(finalTouchPosition,firstTouchPosition) == 0)
            {
                _board.Select(this);
                return;
            }
            if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
            {
                swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
                if (swipeAngle > -45 && swipeAngle <= 45 && y < _board.rows.Length - 1 )
                {
                    //Right Swipe
                    _board.Swipe(this, SwipeDir.Right);

                }
                else if (swipeAngle > 45 && swipeAngle <= 135 && x > 0)
                {
                    //Up Swipe
                    _board.Swipe(this, SwipeDir.Up);

                }
                else if ((swipeAngle > 135 || swipeAngle <= -135) && y > 0)
                {
                    //Left Swipe
                    _board.Swipe(this, SwipeDir.Left);

                }
                else if (swipeAngle < -45 && swipeAngle >= -135 && x < _board.rows[y].tiles.Length - 1)
                {
                    //Down Swipe
                    _board.Swipe(this, SwipeDir.Down);

                }
            }
        }

        public Tween deflate( float tweenDuration)
        {
            var vfx = FactoryObject.Spawn<ParticleSystem>(StringManager.VFXPool, StringManager.DestroyPrefab, this.transform);
            vfx.transform.localScale = new Vector3(25,25,25);
            vfx.transform.localPosition = Vector3.zero;
            return icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack).OnStart(() =>
            {
                vfx.Play();
            }).OnComplete(() =>
            {
                FactoryObject.Despawn(StringManager.VFXPool, vfx.transform, PoolManager.Pools[StringManager.VFXPool].transform);
            });
        }

        public Tween inflate( float tweenDuration)
        {
            return icon.transform.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutBack);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            finalTouchPosition = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
            //firstTouchPosition = finalTouchPosition = Vector2.zero;
        }
    }

    public enum SwipeDir
    {
        Left,Right,Up,Down
    }
}