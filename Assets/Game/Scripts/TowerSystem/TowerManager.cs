using System;
using System.Collections;
using UnityEngine;

namespace Partisan
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private Transform firePointTransform;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float bdc = 0.25f;
        [SerializeField] private float timeBetweenShots = 1f; // Time in seconds between shots

        private Vector3 _enemyPosition;
        private Vector3 _enemyAdjustablePosition;
        private bool _readyToFire = true;

        private void OnTriggerStay(Collider other)
        {
            if (!_readyToFire || !other.CompareTag("Enemy")) return;
            _enemyAdjustablePosition = new Vector3(other.transform.position.x, -(other.transform.position.y - bdc), other.transform.position.z);
            

            _enemyPosition = (_enemyAdjustablePosition - transform.position);


            
            
            Fire(_enemyPosition);
            _readyToFire = false;
        }

        private void Fire(Vector3 enemyRelativePosition)
        {
            var prefabLookRotation = Quaternion.LookRotation(enemyRelativePosition);
            Instantiate(projectilePrefab, firePointTransform.position, prefabLookRotation);
            StartCoroutine(Recharge());
        }

        private IEnumerator Recharge()
        {
            yield return new WaitForSeconds(timeBetweenShots);
            _readyToFire = true;
        }
    }
}
