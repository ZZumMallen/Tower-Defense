using UnityEngine;


namespace Partisan
{
    public class TowerManager : MonoBehaviour, IDragable
    {
        public bool DragAllowed { get; set ; }        

        public void OnMouseDrag()
        {
            if(DragAllowed)
                transform.position = PlacementSystem.instance.GetMouseWorldPosition();
        }
    }
}



/*public bool DragAllowed { get; private set; }

private void OnMouseDrag()
{
    transform.position = PlacementSystem.instance.GetMouseWorldPosition();
}*/