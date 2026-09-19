using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Partisan.Game.EnemySystem
{
    public class EnemyPool : MonoBehaviour
    {
        public static EnemyPool Current { get; private set; }
        
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemiesToSpawn;

        private const int TotalPooledEnemies = 50;
        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            if (Current != null && Current != this)
            {
                Destroy(this);
                return;
            }

            Current = this;
            
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   // helps catch double-release mistakes
                defaultCapacity: enemiesToSpawn,
                maxSize: TotalPooledEnemies
            );
        }

        public void SpawnPooledObject()
        {
            var pooledObject = _pool.Get();
            pooledObject.transform.position = Random.insideUnitSphere * 5f;

            // Return it to the pool after a short delay.
            StartCoroutine(ReturnAfter(pooledObject, 1f));
        }

        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private GameObject CreateItem()
        {
            var pooledObject = enemyPrefab;
            enemyPrefab.SetActive(false);
            return pooledObject;
        }

        // Called when an item is taken from the pool.
        private static void OnGet(GameObject pooledObject)
        {
            pooledObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        private static void OnRelease(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private static void OnDestroyItem(GameObject pooledObject)
        {
            Destroy(pooledObject);
        }

        private IEnumerator ReturnAfter(GameObject pooledObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            // Give it back to the pool.
            _pool.Release(pooledObject);
        }
    }
}
