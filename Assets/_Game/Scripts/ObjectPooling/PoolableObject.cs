using UnityEngine;

namespace Partisan
{
    public class PoolableObject : MonoBehaviour
    {
#pragma warning disable UAC1001 // Public field skipped by serialization due to missing [Serializable]
        public ObjectPool Parent;
#pragma warning restore UAC1001 // Public field skipped by serialization due to missing [Serializable]

        public virtual void OnDisable()
        {
            Parent.ReturnObjectsToPool(this);
        }
    }
}
