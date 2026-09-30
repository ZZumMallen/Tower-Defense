using System;
using UnityEngine;

namespace Partisan
{
    public class Projectile : MonoBehaviour
    {       

        public Transform MyTarget;
        [SerializeField] private float speed;


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
            Destroy(gameObject);
        }
    }
}
