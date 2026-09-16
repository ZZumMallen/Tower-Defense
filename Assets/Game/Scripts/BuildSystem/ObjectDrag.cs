using UnityEngine;

namespace Partisan.Game.BuildSystem
{
    public class ObjectDrag : MonoBehaviour
    {
        private void OnMouseDrag()
        {
            transform.position = BuildingSystem.Current.GetMouseWorldPosition();
        }
    }
}
