using UnityEngine;
using Unity.Properties;

namespace Partisan
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private int myMaxHealth;
        [SerializeField, DontCreateProperty] private float mySpeed;
        [SerializeField, DontCreateProperty] private Vector3 mYTargetOffset;

        public float Speed => mySpeed;
        public int MaxHealth => myMaxHealth;
        public Vector3 TargetOffset => mYTargetOffset;
    }
}
