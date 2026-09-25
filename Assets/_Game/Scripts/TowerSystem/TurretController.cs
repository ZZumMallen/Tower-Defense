using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Partisan
{
    public class TurretController : MonoBehaviour
    {
        private static WaitForSeconds _waitForSeconds0_2 = new WaitForSeconds(0.2f);
        [Title("Attributes")]
        [SerializeField] private float range = 15f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float fireCooldown = 0f;
        [SerializeField] private float rotationSpeed = 2f;

        [Title("Tower Setup Fields")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform partToRotate;
        [SerializeField] private Transform firePoint;

        private Transform _target;
        private GameObject _nearestEnemy = null;
        private readonly string enemyTag = "Enemy";        

        private void Start()
        {
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        private void Update()
        {
            if (_target == null) return;            
            RotateTurretTowardsEnemy();

            if (fireCooldown > 0f) return;                           
            Shoot();

            fireCooldown = 1f / fireRate;    
            fireCooldown -= Time.deltaTime;
        }

        private void Shoot()
        {           
            var newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletController bulletController = newBullet.GetComponent<BulletController>();

            if (bulletController != null) 
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
            var enemyList = GameObject.FindGameObjectsWithTag(enemyTag);
            var _shortestDistance = Mathf.Infinity;

            foreach (var enemy in enemyList)
            {
                var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy < _shortestDistance)
                {
                    _shortestDistance = distanceToEnemy;
                    _nearestEnemy = enemy;
                }
            }

            if (_nearestEnemy != null && _shortestDistance <= range)
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
