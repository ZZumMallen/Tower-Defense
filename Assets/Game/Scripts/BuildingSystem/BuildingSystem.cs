using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.Scripts.BuildingSystem
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem Current { get; private set; }

        [SerializeField] private LayerMask placementLayerMask;
        [SerializeField] private GameObject prefab0;
        [SerializeField] private GameObject prefab1;
        
        
        
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
            var position = GetMouseWorldPosition();
            var obj = Instantiate(prefab, position, Quaternion.identity);
            
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
