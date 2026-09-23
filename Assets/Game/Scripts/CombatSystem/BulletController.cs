using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Partisan
{
    public class BulletController : MonoBehaviour
    {
        private Transform _target;
        [SerializeField] private float speed;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if( _target == null)
            {
                Destroy(gameObject);
                return;
            }

            var dir = _target.position - transform.position;
            var distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame) 
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        private void HitTarget()
        {
            Debug.Log("Hit something");
            Destroy(gameObject);
        }
    }
}
