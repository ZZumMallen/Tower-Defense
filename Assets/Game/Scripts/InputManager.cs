using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private bool debug;
        [SerializeField] private Camera sceneCamera;
        [SerializeField] private LayerMask placementLayerMask;

        private Vector3 _lastPosition;

        /// <summary>
        ///     This function returns the map position of the mouse cursor
        /// </summary>
        /// <returns>Vector3 position</returns>
        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = sceneCamera.nearClipPlane;
            var ray = sceneCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out var hit, 100f, placementLayerMask)) _lastPosition = hit.point;

            return _lastPosition;
        }
    }
}