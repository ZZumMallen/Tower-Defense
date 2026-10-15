using UnityEngine;

namespace Partisan
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileDataSO data;
        public Transform MyTarget;
        private float _speed;
        private int _damage;


        private void Awake()
        {
            _speed = data.Speed;
            _damage = data.Damage;
        }

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
            var distanceThisFrame = _speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out IDamageable damageable)) return;
            damageable.Damage(_damage);
            Destroy(gameObject);
        }
    }
}
