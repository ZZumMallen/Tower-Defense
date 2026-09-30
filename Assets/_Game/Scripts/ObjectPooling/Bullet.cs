using UnityEngine;

namespace Partisan
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : PoolableObject
    {
        [HideInInspector]
        public Rigidbody rb;
        public Vector3 Speed = new(200f, 0f, 0f);

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void OnEnable()
        {
            rb.linearVelocity = Speed;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            rb.linearVelocity = Vector3.zero;
        }
    }
}
