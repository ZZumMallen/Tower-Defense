using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.BuildSystem
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem Current { get; private set; }

        [SerializeField] private LayerMask placementLayerMask;
        [SerializeField] private GameObject currentPrefab0;
        [SerializeField] private GameObject currentPrefab1;
        
        public GameObject Prefab0 { get; private set; }
        public GameObject Prefab1 { get; private set; }

        private Vector3 _lastPosition;
        private Vector3 _initPosition;
        private PlaceableObject _objectToPlace;
        
        #region UnityScripts 
        private void Awake()
        {
            if (Current != null && Current != this)
            {
                Destroy(this);
                return;
            }

            Current = this;

            Prefab0 = currentPrefab0;
            Prefab1 = currentPrefab1;


        }
        #endregion

        public Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null) return _lastPosition;
            var mouseVal = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(mouseVal);

            if (Physics.Raycast(ray, out var hit, 100f, placementLayerMask)) _lastPosition = hit.point;
            Debug.Log(_lastPosition);
            return _lastPosition;
        }

        private bool CanBePlaced()
        {
            var mouseVal = Mouse.current.position.ReadValue();
            if (Camera.main == null) return false;

            var ray = Camera.main.ScreenPointToRay(mouseVal);
            return Physics.Raycast(ray, 100f, placementLayerMask);
        }

        public void InitializeWithObject(GameObject prefab)
        {
            var position = Vector3.zero;
            var obj = Instantiate(prefab, position, Quaternion.identity);
            foreach (var child in obj.GetComponentsInChildren<MeshRenderer>())
            {
                child.enabled = true;
            }
            
            //EnableAllChildren(prefab);
            
            _objectToPlace = obj.GetComponent<PlaceableObject>();
            
            //private virtual void
            obj.AddComponent<ObjectDrag>();
        }

        public void PlaceObject(PlaceableObject obj)
        {
            if (!CanBePlaced())
            {
                Debug.Log($"Can't place object {obj.name}", this);
                return;
            }
            _objectToPlace.Place();
        }
    }
}
