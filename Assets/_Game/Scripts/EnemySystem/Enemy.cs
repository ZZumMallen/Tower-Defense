using UnityEngine;
using UnityEngine.AI;

namespace Partisan
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IDamageable
    {    

        [SerializeField] private EnemyDataSO dataSO;
        [SerializeField] private HealthBar healthBarPrefab;
        [SerializeField] private EnemyOffsetTarget offsetTargetPrefab;
        [SerializeField] private Vector3 healthBarOffset = new Vector3(0f, 2.5f, 0f);

        private NavMeshAgent _agent;

        private float _maxHealth;
        private float _speed;
        private float _currentHealth;
        private Vector3 _yOffset;

        private void Awake()
        {  
            _agent = GetComponent<NavMeshAgent>();
            _speed = dataSO.Speed;
            _maxHealth = dataSO.MaxHealth;
            _yOffset = dataSO.TargetOffset;            
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            var target = Instantiate(offsetTargetPrefab, transform);
            target.transform.localPosition = _yOffset;

            var bar = Instantiate(healthBarPrefab, transform);
            bar.transform.localPosition = healthBarOffset;
            bar.Bind(this);            
        }

        public void SetAgentTarget(GameObject navAgentTarget)
        {
            _agent.SetDestination(navAgentTarget.transform.position);
            _agent.speed = _speed;
        }

        public Transform GetOffsetTarget()
        {
            return offsetTargetPrefab.transform;
        }

        public void Damage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0) Destroy(gameObject);
        }

    }
}
