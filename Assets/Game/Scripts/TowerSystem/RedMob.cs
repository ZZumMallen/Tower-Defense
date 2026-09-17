using UnityEngine;
using UnityEngine.AI;

namespace Partisan
{
    public class RedMob : MonoBehaviour
    {
        [SerializeField] private GameObject navTarget;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _agent.SetDestination(navTarget.transform.position);
        }


    }
}
