using System;
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
        private Transform _offsetTarget;
        private float _maxHealth;
        private float _speed;
        private float _currentHealth;
        private Vector3 _targetOffset;

        /// <summary>Raised whenever this enemy's current health changes.</summary>
        public event Action<float, float> HealthChanged;

        /// <summary>Gets the enemy's current runtime health.</summary>
        public float CurrentHealth => _currentHealth;

        /// <summary>Gets the maximum health configured for this enemy type.</summary>
        public float MaxHealth => _maxHealth;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (dataSO == null)
            {
                Debug.LogError($"Enemy data is not assigned on {name}.", this);
                enabled = false;
                return;
            }

            _speed = dataSO.Speed;
            _maxHealth = Mathf.Max(1, dataSO.MaxHealth);
            _targetOffset = dataSO.TargetOffset;
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            if (offsetTargetPrefab != null)
            {
                var target = Instantiate(offsetTargetPrefab, transform);
                target.transform.localPosition = _targetOffset;
                _offsetTarget = target.transform;
            }

            if (healthBarPrefab != null)
            {
                var healthBar = Instantiate(healthBarPrefab, transform);
                healthBar.transform.localPosition = healthBarOffset;
                healthBar.Bind(this);
            }
            else
            {
                Debug.LogWarning($"Health bar prefab is not assigned on {name}.", this);
            }
        }

        /// <summary>Sets this enemy's navigation destination and movement _speed.</summary>
        public void SetAgentTarget(GameObject navAgentTarget)
        {
            if (navAgentTarget == null)
            {
                Debug.LogWarning($"Cannot set a navigation target for {name}: target is null.", this);
                return;
            }

            _agent.speed = _speed;
            _agent.SetDestination(navAgentTarget.transform.position);
        }

        /// <summary>Gets the live offset target instantiated for this enemy.</summary>
        public Transform GetOffsetTarget()
        {
            return _offsetTarget != null ? _offsetTarget : transform;
        }

        /// <summary>Applies _damage and notifies the bound health bar when health changes.</summary>
        public void Damage(int damage)
        {
            if (damage <= 0 || _currentHealth <= 0f)
            {
                return;
            }

            _currentHealth = Mathf.Max(0f, _currentHealth - damage);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
