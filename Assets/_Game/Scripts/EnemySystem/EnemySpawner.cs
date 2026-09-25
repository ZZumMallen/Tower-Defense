using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnCooldown = 2f;
        [SerializeField] private float enemiesPerWave = 5f;

        private GameObject _agentTarget;

        private void Awake()
        {
            if(_agentTarget == null)
            {
                _agentTarget = FindAnyObjectByType<EnemyTargetLocator>().gameObject;
            }
        }

        [Button]
        public void SpawnWave()
        {    
            InvokeRepeating(nameof(Deploy), 0f, spawnCooldown);
        }

        private void Deploy()
        {
            if (enemiesPerWave > 0f)
            {
                var t = Instantiate(enemyPrefab, gameObject.transform);
                EnemyController controller = t.GetComponent<EnemyController>();
                controller.SetAgentTarget(_agentTarget);
                enemiesPerWave -= 1;
            }
        }
    }
}
