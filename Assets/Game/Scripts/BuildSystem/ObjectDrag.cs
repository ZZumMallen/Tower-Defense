using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.BuildSystem
{
    public class ObjectDrag : MonoBehaviour
    {
        private Vector3 _offset;        

        private void OnMouseDown()
        {
            _offset = transform.position - BuildingSystem.Current.GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            Cursor.visible = false;
            transform.position = BuildingSystem.Current.GetMouseWorldPosition() + _offset;
        }

        private void OnMouseUp()
        {
            Cursor.visible = true;
        }
    }
}
