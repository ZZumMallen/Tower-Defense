using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Partisan
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem current;

        private InputAction _keyboardA;
        private InputAction _keyboardB;

        public GridLayout gridLayout;
        private Grid _grid;
        [SerializeField] private Tilemap mainTileMap;
        [SerializeField] private TileBase whiteTile;
        [SerializeField] private LayerMask placementLayer;

        private Vector3 _lastPosition;

        public GameObject prefab1;
        public GameObject prefab2;

        private PlaceableObject objectToPlace;

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
        }

        #endregion

        #region Utils

        public Vector3 GetMouseWorldPosition()
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit, 100f, placementLayer)) _lastPosition = hit.point;
            
            return _lastPosition;
        
        }


        public Vector3 SnapCoordinateToGrid(Vector3 position)
        {
            Vector3Int cellPos = gridLayout.WorldToCell(position);
            position = _grid.GetCellCenterWorld(cellPos);

            return position;
        }

        #endregion

        #region Building Placement

        public void InitializeWithObject(GameObject prefab)
        {
            var position = SnapCoordinateToGrid(Vector3.zero);

            var obj = Instantiate(prefab, position, Quaternion.identity);

            objectToPlace = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<ObjectDrag>();

        }




        #endregion
    }
}
