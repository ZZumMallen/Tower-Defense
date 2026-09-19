using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan.Game.BuildSystem
{
    public class ObjectDrag : MonoBehaviour
    {
        private Collider _collider;

        private void Start()
        {
            _collider = gameObject.GetComponentInParent<Collider>();
        }
        
        private void OnMouseDrag()
        {
            if (_collider.CompareTag("Tower"))
            {
                transform.position = BuildingSystem.Current.GetMouseWorldPosition();
            }
        }
    }
}
