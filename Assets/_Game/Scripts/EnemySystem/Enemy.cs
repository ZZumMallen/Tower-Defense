using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Partisan
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IDamageable
    {     
        [SerializeField] private EnemyDataSO dataSO;

        private NavMeshAgent _agent;

        private float _maxHealth;
        private float _speed;
        private float _currentHealth;

        private void Awake()
        {  
            _agent = GetComponent<NavMeshAgent>();
            _speed = dataSO.Speed;
            _maxHealth = dataSO.MaxHealth;          
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void SetAgentTarget(GameObject navAgentTarget)
        {
            _agent.SetDestination(navAgentTarget.transform.position);
            _agent.speed = _speed;
        }

        public void Damage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0) Destroy(gameObject);
        }

    }
}
