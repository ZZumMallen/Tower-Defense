using UnityEngine;

namespace Partisan
{
    public interface IDragable
    {
        public bool DragAllowed { get; set; }
        void OnMouseDrag();
    }
}
