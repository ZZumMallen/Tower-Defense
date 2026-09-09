using UnityEngine;

namespace Partisan.Game.Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera sceneCamera;
        [SerializeField] private LayerMask placementLayerMask;
        [SerializeField] private int rayMaxDistance = 100;
        
        private Vector3 _lastMousePosition;

        public Vector3 GetSelectedMapPosition()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = sceneCamera.nearClipPlane;
            
            var ray = sceneCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, rayMaxDistance, placementLayerMask))
            {
                _lastMousePosition = hitInfo.point;
            }

            return _lastMousePosition;
        }
         
    }
}
