using System;
using UnityEngine;

namespace Partisan
{
    [RequireComponent(typeof(BoxCollider))]
    public class LoseCondition : MonoBehaviour
    {
        public event Action LossConditionMet;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy")) 
            {
                Debug.LogWarning("YOU LOSE");
                LossConditionMet?.Invoke();
            }
        }
    }
}
