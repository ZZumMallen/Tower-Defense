using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Partisan
{
    public class TowerGenerator : MonoBehaviour
    {
        [SerializeField] private LayerMask placementLayer;
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float verticalOffset = 0.5f;

        private void Start()
        {
            BuildUI.Instance.OnRequestTower1 += InitializeWithObject;
            BuildUI.Instance.OnRequestTower2 += InitializeWithObject;
            BuildUI.Instance.OnRequestTower3 += InitializeWithObject;
        }

        private void OnDestroy()
        {
            BuildUI.Instance.OnRequestTower1 -= InitializeWithObject;
            BuildUI.Instance.OnRequestTower2 -= InitializeWithObject;
            BuildUI.Instance.OnRequestTower3 -= InitializeWithObject;
        }

        [Button]
        private void InitializeWithObject()
        {
            var initPosition = new Vector3(0f, verticalOffset, 0f);
            Instantiate(prefabToSpawn, initPosition, Quaternion.identity);
        }
    }
}
