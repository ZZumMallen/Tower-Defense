using UnityEngine;
using UnityEngine.AI;

namespace Partisan
{
    public class Enemy : MonoBehaviour
    {        
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SetAgentTarget(GameObject navAgentTarget)
        {
            _agent.SetDestination(navAgentTarget.transform.position);
        }


    }
}
