using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

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

        private Quaternion _startRotation;
        private Vector3 _dummyStartPosition;


        private GameObject[] _enemyList;
        private GameObject _targetSolution;
        private bool _isReadytoShoot;

        private GameObject _nearestEnemy;
        private const string EnemyTag = "Enemy";


        private void Awake()
        {
            _startRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void Start()
        {
            var p = gameObject.transform.position;
            _dummyStartPosition = new Vector3(p.x, p.y, p.z + 2);

            _isReadytoShoot = true;
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        private void Update()
        {
            if (!_targetSolution)
            {
                RotateTurretTowardsIdle();
                return;
            }
            else
            {
                RotateTurretTowardsEnemy();
            }            

            if(_isReadytoShoot)
            {
                ShootAt(_targetSolution);
                _isReadytoShoot = false;
            }
        }

        private void ShootAt(GameObject obj)
        {            
            Debug.Log("ShootAtCalled");
            var newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            var projectile = newProjectile.GetComponent<Projectile>();
            projectile.MyTarget = obj.transform;
            StartCoroutine(WeaponCooldown());
        }

        private IEnumerator WeaponCooldown()
        {
            yield return new WaitForSeconds(fireCooldown);
            yield return _isReadytoShoot = true;
        }

        private void RotateTurretTowardsEnemy()
        {
            var dir = _targetSolution.transform.position - transform.position;
            var lookRotation = Quaternion.LookRotation(dir);
            var rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        private void RotateTurretTowardsIdle()
        {
            var dist = Mathf.Abs(_startRotation.eulerAngles.y - turret.rotation.eulerAngles.y);

            if (dist < 0.5f) return; 

            Debug.LogWarning("Rotate towards Idle");

            var dir = _dummyStartPosition - transform.position;
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

                if(distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    _nearestEnemy = enemy;
                }
            }
                
            if (_nearestEnemy != null && shortestDistance <= effectiveRange)
            {
                _targetSolution = _nearestEnemy;
            }
            else
            {
                _targetSolution = null;
            }             

            if (_nearestEnemy == null) return;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, effectiveRange);
        }
    }
}
