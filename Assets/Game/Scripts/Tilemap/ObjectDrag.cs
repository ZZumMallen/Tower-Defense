using UnityEngine;

namespace Partisan
{
    public class ObjectDrag : MonoBehaviour
    {
        private Vector3 _offset;

        private void OnMouseDown()
        {
            _offset = transform.position - BuildingSystem.current.GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            Vector3 pos = BuildingSystem.current.GetMouseWorldPosition() + _offset;
            transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
        }
    }
}
