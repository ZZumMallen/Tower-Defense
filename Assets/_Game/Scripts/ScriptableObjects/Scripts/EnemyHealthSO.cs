using UnityEngine;

namespace Partisan
{
    [CreateAssetMenu(fileName = "EnemyHealthSO", menuName = "Scriptable Objects/EnemyHealthSO")]
    public class EnemyHealthSO : ScriptableObject
    {
        public int maxHealth = 100;
        public int currentHealth;

        public void Reset()
        {
            currentHealth = maxHealth;
        }
    }
}
