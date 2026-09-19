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

            
            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                Destroy(gameObject);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Enemy")) return;
            Destroy(gameObject);
            Debug.Log($"Deal 10 damage to {collision.gameObject.name}");
        }
    }
}
