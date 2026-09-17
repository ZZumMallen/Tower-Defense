using UnityEngine;

namespace Partisan
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (!_target) return;

            var direction = (_target.position - transform.position).normalized;
            transform.position += direction * (speed * Time.deltaTime);

            // Optionally, destroy the projectile if it reaches the target
            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                Destroy(gameObject);
            }

        }
    }
}
