using UnityEngine;

namespace Partisan
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        public Transform target { get; set; }



        public void SetTarget(Transform target)
        {
            this.target = target;
            //Debug.Log($"Target set to: {target.name}");
        }

        void Update()
        {
            if (target == null) return;

            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;

            // Optionally, destroy the projectile if it reaches the target
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(gameObject);
            }

        }
    }
}
