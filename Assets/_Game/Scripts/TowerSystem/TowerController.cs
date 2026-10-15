using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan
{
    public class TowerController : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _mousePositionOffset;
        [SerializeField] private LayerMask layerMask;
        [ShowInInspector] private Vector3 _lastPosition;
        [ShowInInspector] private Vector2 _mouseValue;
        [SerializeField] private bool useGridPlacement = true; 

        private void Awake()
        {
            _camera = Camera.main;
        }

        private Vector3 GetMouseWorldPosition()
        {
            _mouseValue = Mouse.current.position.ReadValue();
            var ray = _camera.ScreenPointToRay(_mouseValue);
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask)) _lastPosition = hit.point;

            if (useGridPlacement)
            {
                var pX = Mathf.Floor(hit.point.x) + 0.5f;
                var pY = hit.point.y;
                var pZ = Mathf.Floor(hit.point.z) + 0.5f;
                
                _lastPosition = new Vector3(pX, pY, pZ);
            }
            
            return _lastPosition;
        }

        private void OnMouseDown()
        {
            _mousePositionOffset = transform.position - GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPosition() + _mousePositionOffset;
        }
    }
}

