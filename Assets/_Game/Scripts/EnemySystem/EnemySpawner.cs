using System.Collections;
using UnityEngine;



namespace Partisan
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private GameObject agentTarget;
        //[SerializeField] private float spawnCooldown = 2f;
        //[SerializeField] private float enemiesPerWave = 5f;

        private TestingUI _ui;

        private void Awake()
        {
            _ui = GetComponent<TestingUI>();          
        }

        private void Start()
        {
            _ui.OnRequestEnemy += SpawnEnemy;
        }

        private void SpawnEnemy()
        {
            var t = Instantiate(enemyPrefab, enemySpawnPoint);
            Enemy controller = t.GetComponent<Enemy>();
            controller.SetAgentTarget(agentTarget);
        }




    }
}
