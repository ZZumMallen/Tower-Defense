using Unity.Properties;
using UnityEngine;

namespace Partisan
{
    [CreateAssetMenu(fileName = "EnemyWaveDataSO", menuName = "Scriptable Objects/EnemyWaveDataSO")]
    public class EnemyWaveDataSO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private float _spawnCooldown;
        [SerializeField, DontCreateProperty] private int _waveCount;

        public int WaveCount => _waveCount;
        public float SpawnCooldown => _spawnCooldown;
    }
}
