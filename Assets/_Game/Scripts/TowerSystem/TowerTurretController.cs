using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{
    public class TowerTurretController : MonoBehaviour
    {
        [Title("Attributes")]
        [SerializeField] private float range = 15f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float fireCooldown;
        [SerializeField] private float rotationSpeed = 2f;

        [Title("Tower Setup Fields")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform partToRotate;
        [SerializeField] private Transform firePoint;

        private Transform _target;
        private GameObject _nearestEnemy;
        private const string EnemyTag = "Enemy";

        private void Start()
        {
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        private void Update()
        {
            if (!_target) return;            
            RotateTurretTowardsEnemy();

            if (fireCooldown > 0f) return;                           
            Shoot();

            fireCooldown = 1f / fireRate;    
            fireCooldown -= Time.deltaTime;
        }

        private void Shoot()
        {           
            var newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            //Todo move the bullet system into an object pool
            var bulletController = newBullet.GetComponent<BulletController>();

            if (bulletController) 
            {
                bulletController.SetTarget(_target);
            }
        }

        private void RotateTurretTowardsEnemy()
        {
            var dir = _target.transform.position - transform.position;
            var lookRotation = Quaternion.LookRotation(dir);
            var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;    
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        private void UpdateTarget()
        {
            var enemyList = GameObject.FindGameObjectsWithTag(EnemyTag);
            var shortestDistance = Mathf.Infinity;

            foreach (var enemy in enemyList)
            {
                var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    _nearestEnemy = enemy;
                }
            }

            if (_nearestEnemy != null && shortestDistance <= range)
            {
                _target = _nearestEnemy.transform;
            }
            else
            {
                _target = null;
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, range);
        }
    }
}
