using System.Collections;
using UnityEngine;

namespace Partisan
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float timeBetweenShots = 1f; // Time in seconds between shots

        private ProjectileController _projectileController;
        private GameObject _currentProjectile;

        private bool _readyToFire = true;

        private void Start()
        {
            if (origin == null)
            {
                Debug.LogError("Origin transform is not assigned.");
            }
            if (projectilePrefab == null)
            {
                Debug.LogError("Projectile prefab is not assigned.");
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_readyToFire) return;

            if (!other.CompareTag("Enemy")) return;
            _currentProjectile = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
            _projectileController = _currentProjectile.GetComponent<ProjectileController>();
            _projectileController.SetTarget(other.transform);

            _readyToFire = false;

            StartCoroutine(Recharge());
        }

        private IEnumerator Recharge()
        {
            yield return new WaitForSeconds(timeBetweenShots);
            _readyToFire = true;
        }
    }
}
