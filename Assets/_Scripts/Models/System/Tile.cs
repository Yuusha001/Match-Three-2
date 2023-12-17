using DG.Tweening;
using NaughtyAttributes;
using PathologicalGames;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace MatchThree
{
    public sealed class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [ReadOnly]
        public Image square;
        [ReadOnly]
        public Image icon;
        [ReadOnly]
        public Image specialIcon;
        private Board _board;
        public int x { get; private set; }
        public int y { get; private set; }
        public TileData Data => new TileData(x, y, _type != null ? _type.id : - 1);
        private TileTypeAsset _type;
        private SpecialTileTypeAsset _specialType;
        [ReadOnly]
        [SerializeField]
        private SquareBlocks squareBlock;
        [ReadOnly]
        [SerializeField]
        private Transform[] borders;
        [ReadOnly]
        [SerializeField]
        private Image obstacle;
        public TileTypeAsset Type
        {
            get => _type;

            set
            {
                if (!CanSpawnTile()) return;

                if (_type == value) return;

                _type = value;
                if(_type == null) return;
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
        private Vector2 firstTouchPosition;
        private Vector2 finalTouchPosition;
        public void Initialize(Board board, int _x, int _y,SquareBlocks _squareBlock)
        {
            _board = board;
            x = _x;
            y = _y;
            SpecialType = null;
            squareBlock = _squareBlock;
            square.sprite = DataManager.Instance.boardSprites[(x + y) % 2 == 0 ? 0 : 1];
            switch (squareBlock.block)
            {
                case EMapType.Normal:
                    square.enabled = false;
                    icon.enabled = false;
                    specialIcon.enabled = false;
                    break;
                case EMapType.Empty:
                    square.enabled = true;
                    icon.enabled = true;
                    specialIcon.enabled = (SpecialType != null);
                    break;
            }
            ObstacleInitialize();
            GetRandomTile();
        }

        private bool CanSpawnTile()
        {
            return squareBlock.block == EMapType.Empty 
                && (squareBlock.obstacle == EMapType.Normal 
                || squareBlock.obstacle == EMapType.Block_1 
                || squareBlock.obstacle == EMapType.Block_2
                || squareBlock.obstacle == EMapType.Frozen);
        }

        public bool CanSwap()
        {
            return squareBlock.block == EMapType.Empty 
                && (squareBlock.obstacle == EMapType.Normal
                || squareBlock.obstacle == EMapType.Block_1
                || squareBlock.obstacle == EMapType.Block_2);
        }

        public bool CanChange()
        {
            return (_type != null);
        }

        public void GetRandomTile()
        {
            if (CanSpawnTile())
            {
                Type = _board.tileTypes[Random.Range(0, _board.tileTypes.Length)];
            }
        }
        public void BorderInitialize()
        {
            foreach (var item in borders)
            {
                item.gameObject.SetActive(false);
            }
            if (squareBlock.block == EMapType.Empty)
            {
                borders[0].gameObject.SetActive(!GetNeighborRight() || GetNeighborRight().squareBlock.block == EMapType.Normal);
                borders[1].gameObject.SetActive(!GetNeighborLeft() || GetNeighborLeft().squareBlock.block == EMapType.Normal);
                borders[2].gameObject.SetActive(!GetNeighborTop() || GetNeighborTop().squareBlock.block == EMapType.Normal);
                borders[3].gameObject.SetActive(!GetNeighborBot() || GetNeighborBot().squareBlock.block == EMapType.Normal);
            }
        }

        public void ObstacleInitialize()
        {
            obstacle.gameObject.SetActive(squareBlock.obstacle != EMapType.Normal);
            if (squareBlock.obstacle == EMapType.Normal)
                return;
            switch (squareBlock.obstacle)
            {
                case EMapType.Block_1:
                    obstacle.sprite = DataManager.Instance.blockSprites[0];
                    icon.transform.SetAsLastSibling();
                    break;
                case EMapType.Block_2:
                    obstacle.sprite = DataManager.Instance.blockSprites[1];
                    icon.transform.SetAsLastSibling();
                    break;
                case EMapType.Rock_1:
                    obstacle.sprite = DataManager.Instance.rockSprites[0];
                    icon.enabled = false;
                    break;
                case EMapType.Rock_2:
                    obstacle.sprite = DataManager.Instance.rockSprites[1];
                    icon.enabled = false;
                    break;
                case EMapType.Frozen:
                    obstacle.sprite = DataManager.Instance.iceSprites;
                    obstacle.transform.SetAsLastSibling();
                    break;
                case EMapType.Thriving:
                    obstacle.sprite = DataManager.Instance.thrivingSprites;
                    icon.enabled = false;
                    break;
            }
        }

        private void RemoveObstacle()
        {
            switch (squareBlock.obstacle)
            {
                case EMapType.Block_1:
                    squareBlock.obstacle = EMapType.Normal;
                    ObstacleInitialize();
                    break;
                case EMapType.Block_2:
                    squareBlock.obstacle = EMapType.Block_1;
                    ObstacleInitialize();
                    break;
                case EMapType.Rock_1:
                    squareBlock.obstacle = EMapType.Normal;
                    ObstacleInitialize();
                    break;
                case EMapType.Frozen:
                    squareBlock.obstacle = EMapType.Normal;
                    ObstacleInitialize();
                    break;
            }
        }

        private Tile GetNeighborLeft()
        {
            if (y == 0)
                return null;
            return _board.GetTile(x , y - 1);
        }

        private Tile GetNeighborRight()
        {
            if (y == _board.rows.Max(row => row.tiles.Length)-1)
                return null;
            return _board.GetTile(x , y + 1);
        }

        private Tile GetNeighborTop()
        {
            if (x == 0)
                return null;
            return _board.GetTile(x - 1, y );
        }

        private Tile GetNeighborBot()
        {
            if (x == _board.rows.Length-1)
                return null;
            return _board.GetTile(x + 1, y );
        }

        private void CalculateAngle()
        {
            if (!CanSwap())
                return;
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
                if (squareBlock.obstacle == EMapType.Normal)
                {
                    vfx.Play();
                }
                else
                {
                    RemoveObstacle();
                }
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

        public void DeInitialize()
        {
            firstTouchPosition = finalTouchPosition = Vector2.zero;
            SpecialType = null;
            Type = null;
            x = y = 0;
        }
    }

    public enum SwipeDir
    {
        Left,Right,Up,Down
    }
}