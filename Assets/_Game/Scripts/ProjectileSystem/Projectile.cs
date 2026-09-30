using UnityEngine;

namespace Partisan
{
    public class Projec : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float speed;

        private void Update()
        {
            if(!target)
            {
                Destroy(gameObject);
                return;
            }

            var dir = target.position - transform.position;
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
