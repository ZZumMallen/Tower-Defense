using UnityEngine;

namespace Partisan
{
    public class Projectile : MonoBehaviour
    {       

        public Transform MyTarget;
        [SerializeField] private float speed;


        public void SetTarget(Transform target)
        {
            MyTarget = target;
        }

        private void Update()
        {
            if(!MyTarget)
            {
                Destroy(gameObject);
                return;
            }

            var dir = MyTarget.position - transform.position;
            var distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out IDamageable damageable)) return;   
            damageable.Damage(10);
        }
    }
}
