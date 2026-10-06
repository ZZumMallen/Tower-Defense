using UnityEngine;
using Unity.Properties;

namespace Partisan
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private int m_maxHealth;
        [SerializeField, DontCreateProperty] private float m_speed;
        [SerializeField, DontCreateProperty] private Vector3 m_yTargetOffset;

        public float Speed => m_speed;
        public int MaxHealth => m_maxHealth;
        public Vector3 TargetOffset => m_yTargetOffset;
    }
}
