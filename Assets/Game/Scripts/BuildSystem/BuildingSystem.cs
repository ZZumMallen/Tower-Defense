using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.BuildSystem
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem Current { get; private set; }

        [SerializeField] private LayerMask placementLayerMask;
        public GameObject prefab0 { get; private set; }
        public GameObject prefab1 { get; private set; }

        [SerializeField] private GameObject currentPrefab0;
        [SerializeField] private GameObject currentPrefab1;

        private Vector3 _lastPosition;
        private PlaceableObject _objectToPlace;

        #region UnityScripts 
        private void Awake()
        {
            if(Current != null && Current != this)
            {
                Destroy(this);
                return;
            }

            Current = this;

            prefab0 = currentPrefab0;
            prefab1 = currentPrefab1;

            
        }
        #endregion

        public Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null) return _lastPosition;
            var mouseVal = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(mouseVal);

            if (Physics.Raycast(ray, out var hit, 100f, placementLayerMask)) _lastPosition = hit.point;
            return _lastPosition;
        }

        public bool CanBePlaced()
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
            obj.GetComponentInChildren<MeshRenderer>().enabled = true;

            _objectToPlace = obj.GetComponent<PlaceableObject>();


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
        
        //This would be the place to add the grid system if needed later
    }
}
