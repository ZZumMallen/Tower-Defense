using UnityEngine;

namespace Partisan.Game.Scripts.BuildingSystem
{
    public class ObjectDrag : MonoBehaviour
    {
        private void OnMouseDrag()
        {
            transform.position = BuildingSystem.Current.GetMouseWorldPosition();
        }
    }
}
