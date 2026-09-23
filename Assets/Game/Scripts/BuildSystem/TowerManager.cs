using Unity.VisualScripting;
using UnityEngine;

namespace Partisan
{
    public class TowerManager : MonoBehaviour
    {
        private void OnMouseDrag()
        {
            transform.position = PlacementSystem.instance.GetMouseWorldPosition();
        }
    }
}
