using UnityEngine;

namespace Partisan.Game.Scripts.BuildingSystem
{
    public class PlaceableObject : MonoBehaviour
    {
        public bool Placed { get; private set; }

        public virtual void Place()
        {
            var drag = gameObject.GetComponent<ObjectDrag>();
            Destroy(drag);

            Placed = true;
        }
    }
}