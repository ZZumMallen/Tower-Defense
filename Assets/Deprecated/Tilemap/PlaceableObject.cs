using UnityEngine;

namespace Partisan.GameObjects
{
    public class PlaceableObject : MonoBehaviour
    {
        /*public bool Placed { get; private set; }
        public Vector3Int Size { get; private set; }
        private Vector3[] _vertices;
        
        private void GetColliderVertexPositionsLocal()
        {
            var b = gameObject.GetComponent<BoxCollider>();
            _vertices = new Vector3[4];
            _vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f; // ( -x, -z )
            _vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f; // (  x, -z )
            _vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f; // (  x,  z )
            _vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f; // ( -x,  z )
        }
        
        private void CalculateSizeInCells()
        {
            var vertices = new Vector3Int[_vertices.Length];
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            for (var i = 0; i < vertices.Length; i++)
            {
                var worldPos = transform.TransformPoint(_vertices[i]);
                vertices[i] = OldBuildingSystem.current.gridLayout.WorldToCell(worldPos);
            }

            Size = new Vector3Int(
                Math.Abs((vertices[0] - vertices[1]).x), 
                Math.Abs((vertices[0] - vertices[3]).y), 
                1);
        }
        
        public Vector3 GetStartPosition()
        {
            return transform.TransformPoint(_vertices[0]);
        }

        private void Start()
        {
            GetColliderVertexPositionsLocal();
            CalculateSizeInCells();
        }

        public virtual void Place()
        {
            ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
            Destroy(drag);
            
            Placed = true;
            
            //invoke events of placement here
        }*/
    }
}
