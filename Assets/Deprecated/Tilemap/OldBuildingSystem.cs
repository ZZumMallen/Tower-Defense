using UnityEngine;

namespace Partisan
{
    public class OldBuildingSystem : MonoBehaviour
    {
        /*public static OldBuildingSystem current;

        private InputAction _keyboardA;
        private InputAction _keyboardB;
        private InputAction _spaceBar;
        private InputAction _esc;

        public GridLayout gridLayout;
        private Grid _grid;
        [SerializeField] private Tilemap mainTileMap;
        [SerializeField] private TileBase whiteTile;
        [SerializeField] private LayerMask placementLayer;

        private Vector3 _lastPosition;

        public GameObject prefab1;
        public GameObject prefab2;

        private PlaceableObject _objectToPlace;

        #region Unity Methods

        private void Awake()
        {
            if(current == null)
            {
                current = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _grid = gridLayout.gameObject.GetComponent<Grid>();
            _keyboardA = InputSystem.actions.FindAction("KeyboardA");
            _keyboardB = InputSystem.actions.FindAction("KeyboardB");
            _spaceBar = InputSystem.actions.FindAction("SpaceBar");
            _esc = InputSystem.actions.FindAction("Esc");
        }

        private void Update()
        {
            if (_keyboardA.WasPerformedThisFrame())
            {
                InitializeWithObject(prefab1);
            }

            if (_keyboardB.WasPerformedThisFrame())
            {
                InitializeWithObject(prefab2);
            }

            if (!_objectToPlace)
            {
                return;
            }

            if (_spaceBar.WasCompletedThisFrame())
            {
                if (!CanBePlaced(_objectToPlace))
                {
                    Debug.Log("Cannot be placed dude", this);
                    return;
                }
                
                Debug.Log("Can be placed dude", this);
                
                _objectToPlace.Place();
                var start = gridLayout.WorldToCell(_objectToPlace.GetStartPosition());
                TakeArea(start, _objectToPlace.Size);
            }
            else if (_esc.WasCompletedThisFrame())
            {
                Destroy(_objectToPlace.gameObject);
            }
        }

        #endregion

        #region Utils

        public Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null) return _lastPosition;
            
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit, 100f, placementLayer)) _lastPosition = hit.point;

            return _lastPosition;
        
        }

        public Vector3 SnapCoordinateToGrid(Vector3 position)
        {
            var cellPos = gridLayout.WorldToCell(position);
            position = _grid.GetCellCenterWorld(cellPos);

            return position;
        }

        private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
        {
            var array = new TileBase[area.size.x * area.size.y * area.size.z];
            var counter = 0;

            foreach (var v in area.allPositionsWithin)
            {
                var pos = new Vector3Int(v.x, v.y, 0);
                array[counter] = tilemap.GetTile(pos);
                counter++;
            }
            return array; }
        #endregion

        //-----------------------------------------------------------------------------------------------
        
        #region Building Placement

        // ReSharper disable Unity.PerformanceAnalysis
        public void InitializeWithObject(GameObject prefab)
        {
            var position = SnapCoordinateToGrid(Vector3.zero);

            var obj = Instantiate(prefab, position, Quaternion.identity);

            _objectToPlace = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<ObjectDrag>();

        }

        private bool CanBePlaced(PlaceableObject placeableObject)
        {
            var area = new BoundsInt
            {
                position = gridLayout.WorldToCell(_objectToPlace.GetStartPosition()),
                size = placeableObject.Size
            };

            var baseArray = GetTilesBlock(area, mainTileMap);

            foreach (var b in baseArray)
            {
                if (b == whiteTile) return false;
            }

            return true;
        }

        private void TakeArea(Vector3Int start, Vector3Int size)
        {
            mainTileMap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
        }




        #endregion*/
    }
}
