using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.BuildSystem
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem Current { get; private set; }
        public GameObject TowerPrefab1 { get; private set; }
        public GameObject TowerPrefab2 { get; private set; }

        [SerializeField] private LayerMask placementLayerMask;
        [SerializeField] private GameObject towerPrefab1;
        [SerializeField] private GameObject towerPrefab2;
        [SerializeField] private GameObject ground;

        private Vector3 _lastPosition;
        private float _groundOffset;
        
        #region UnityScripts 
        private void Awake()
        {
            if (Current != null && Current != this)
            {
                Destroy(this);
                return;
            }
            Current = this;

            TowerPrefab1 = towerPrefab1;
            TowerPrefab2 = towerPrefab2;
        }

        private void Start()
        {
            _groundOffset = ground.transform.localScale.y / 2f;
        }

        #endregion

        public Vector3 GetMouseWorldPosition()
        {            
            var mouseVal = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(mouseVal);

            if (Physics.Raycast(ray, out var hit, 100f, placementLayerMask)) _lastPosition = hit.point;

            return _lastPosition;
        }



        public bool CanBePlaced()
        {
            var mouseVal = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(mouseVal);
            return Physics.Raycast(ray, 100f, placementLayerMask);
        }

        
        public void InitializeWithObject(GameObject prefab)
        {
            var initPosition = Vector3.zero;
            var obj = Instantiate(prefab, initPosition, Quaternion.identity);

            foreach (var child in obj.GetComponentsInChildren<MeshRenderer>())
            {
                child.enabled = true;
            }

            //private virtual void
            obj.AddComponent<ObjectDrag>(); 
        }
    }
}
