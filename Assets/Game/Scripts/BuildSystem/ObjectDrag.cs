using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.BuildSystem
{
    public class ObjectDrag : MonoBehaviour
    {
        
        
        private void OnMouseDown()
        {
            var pos = transform.position;
            var mousePos = BuildingSystem.Current.GetMouseWorldPosition();
            var gameObjectName = gameObject.name;
            
            Debug.Log($"{gameObjectName} is at {pos} and mouse is at {mousePos}");
        }
        
        private void OnMouseDrag()
        {
            transform.position = BuildingSystem.Current.GetMouseWorldPosition();
        }
    }
}
