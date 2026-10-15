using Unity.Properties;
using UnityEngine;

namespace Partisan
{
    [CreateAssetMenu(fileName = "ProjectileDataSO", menuName = "Scriptable Objects/ProjectileDataSO")]
    public class ProjectileDataSO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private float _speed;
        [SerializeField, DontCreateProperty] private int _damage;

        public float Speed => _speed;
        public int Damage => _damage;
    }
}
