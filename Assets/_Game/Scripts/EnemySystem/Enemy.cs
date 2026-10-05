using UnityEngine;
using UnityEngine.AI;

namespace Partisan
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IDamageable
    {        
        private NavMeshAgent _agent;
        private float _maxHealth;
        private float _currentHealth;
        private float _speed;

        [SerializeField] private EnemyData enemyData;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _speed = enemyData.Speed;

        }

        private void Start()
        {
            _maxHealth = enemyData.MaxHealth;
            //CurrentHealth = _currentHealth;
        }

        public void SetAgentTarget(GameObject navAgentTarget)
        {
            _agent.SetDestination(navAgentTarget.transform.position);
            _agent.speed = _speed;
        }

        public void Damage(int damage)
        {
            _currentHealth -= damage;


            if (_currentHealth <= 0) 
            {
                Destroy(gameObject);
            }
        }
    }
}
