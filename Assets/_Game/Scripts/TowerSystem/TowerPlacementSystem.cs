using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan
{
    public class TowerPlacementSystem : MonoBehaviour
    {
        public static TowerPlacementSystem Instance;

        [SerializeField] private LayerMask placementLayer;
        [SerializeField] private GameObject testPrefab;
        [SerializeField] private Camera camera;
        [SerializeField] private float verticalOffset = 0.5f;

        private Vector3 _lastPosition;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }

        public Vector3 GetMouseWorldPosition()
        {
            var mouseVal = Mouse.current.position.ReadValue();
            var ray = camera.ScreenPointToRay(mouseVal);

            if (Physics.Raycast(ray, out var hit, 100f, placementLayer)) _lastPosition = hit.point;

            return _lastPosition;
        }

        [Button]
        public void InitializeWithObject()
        {
            var initPosition = new Vector3(0f, verticalOffset, 0f);
            Instantiate(testPrefab, initPosition, Quaternion.identity);

        }
    }
}
