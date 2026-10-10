using UnityEngine;
using System.Threading;
namespace Partisan
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private GameObject agentTarget;

        [SerializeField] private EnemyWaveDataSO data;
        private float _spawnCooldown;
        private int _waveCount;

        private DebugUI _ui;

        private void Awake()
        {
            _ui = GetComponent<DebugUI>();
            _spawnCooldown = data.SpawnCooldown;
            _waveCount = data.WaveCount;
        }

        private void Start()
        {
            _ui.OnRequestEnemy += SpawnEnemy;
            _ui.OnRequestWave += SpawnEnemyWave;
        }

        private void SpawnEnemy()
        {
            var t = Instantiate(enemyPrefab, enemySpawnPoint);
            Enemy controller = t.GetComponent<Enemy>();
            controller.SetAgentTarget(agentTarget);
        }

        private async void SpawnEnemyWave()
        {
            for (int i = 0; i < _waveCount; i++)
            {
                var t = Instantiate(enemyPrefab, enemySpawnPoint);
                Enemy controller = t.GetComponent<Enemy>();
                controller.SetAgentTarget(agentTarget);
                await Awaitable.WaitForSecondsAsync(_spawnCooldown, CancellationToken.None);
            }
        }
    }
}
