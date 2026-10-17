using System;
using UnityEngine;

namespace Partisan
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents Instance;

        

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public event Action<Vector3> OnEnemySpawnAdded;
        public void EnemySpawnPointCreated(Vector3 point)
        {
            Debug.Log("SPAWN POINT CREATED");
            OnEnemySpawnAdded?.Invoke(point);
        }

        public event Action<Vector3> OnEnemyTargetAdded;
        public void EnemyTargetPointCreated(Vector3 point)
        {
            Debug.Log("TARGET POINT CREATED");
            OnEnemyTargetAdded?.Invoke(point);
        }
    }
}