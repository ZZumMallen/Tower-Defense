using UnityEngine;
using UnityEngine.AI;

namespace Partisan
{
    public class RedMob : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            _agent.SetDestination(_target.transform.position);
        }


    }
}
