using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{
    public class TowerTurretController : MonoBehaviour
    {
        [Title("Attributes")]
        [SerializeField] private float effectiveRange = 15f;
        [SerializeField] private float fireCooldown;
        [SerializeField] private float rotationSpeed = 2f;

        [Title("Tower Setup Fields")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform turret;
        [SerializeField] private Transform firePoint;


        private GameObject[] _enemyList;
        private GameObject _targetSolution;
        private bool _readyToShoot;

        private GameObject _nearestEnemy;
        private const string EnemyTag = "Enemy";


        private void Start()
        {
            _readyToShoot = true;
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        private void Update()
        {
            if (!_targetSolution) return;
            RotateTurretTowardsEnemy();

            if (!_readyToShoot) return;
            
            ShootAt(_targetSolution);
            _readyToShoot = false;
            
        }

        private void ShootAt(GameObject obj)
        {            
            var newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            var projectile = newProjectile.GetComponent<Projectile>();
            projectile.MyTarget = obj.transform;
            StartCoroutine(WeaponCooldown());
        }

        private IEnumerator WeaponCooldown()
        {
            yield return new WaitForSeconds(fireCooldown);
            yield return _readyToShoot = true;
        }

        private void RotateTurretTowardsEnemy()
        {
            if (!_targetSolution) return;

            var dir = _targetSolution.transform.position - transform.position;
            var lookRotation = Quaternion.LookRotation(dir);
            var rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        private void UpdateTarget()
        {
            _enemyList = GameObject.FindGameObjectsWithTag(EnemyTag);
            var shortestDistance = Mathf.Infinity;

            foreach (var enemy in _enemyList)
            {
                var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    _nearestEnemy = enemy;
                }
            }

            if (_nearestEnemy == null) return;

            ValidateTarget(shortestDistance, _nearestEnemy);

        }

        private GameObject ValidateTarget(float shortest, GameObject nearest)
        {
            if(nearest != null && shortest <= effectiveRange)
            {
                _targetSolution = nearest;
            }
            else 
            {
                _targetSolution=null;
            }

            return _targetSolution;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, effectiveRange);
        }
    }
}
