using UnityEngine;
using Unity.Properties;

namespace Partisan
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        private Enemy k_myEnemy;


        [SerializeField, DontCreateProperty] private string m_enemyName;
        [SerializeField, DontCreateProperty] private int m_maxHealth;
        [SerializeField, DontCreateProperty] private int m_currentHealth;
        [SerializeField, DontCreateProperty] private float m_speed;

        [CreateProperty] public string EnemyName => m_enemyName;
        [CreateProperty] public float Speed => m_speed;
        [CreateProperty] public int CurrentHealth => Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
        [CreateProperty] public int MaxHealth => m_maxHealth;
        [CreateProperty] public float HealthPercentage => (float)CurrentHealth / m_maxHealth;
        [CreateProperty] public string MaximumHealthFormatted => $" / {MaxHealth}";


    }
}
