using UnityEngine;


namespace Partisan
{
    public class TowerManager : MonoBehaviour
    {
        public bool DragAllowed { get; set ; }        

        public void OnMouseDrag()
        {
            if(DragAllowed)
                transform.position = TowerPlacementSystem.Instance.GetMouseWorldPosition();
        }
    }
}

