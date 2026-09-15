using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Partisan
{
    public class GridMouseData : MonoBehaviour
    {
        public static GridMouseData Instance { get; private set; }
        [SerializeField] private Camera sceneCamera;
        [SerializeField] private LayerMask placementLayerMask;

        private Vector3 _lastPosition;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;    

        }


        public Vector3 GetSelectedMapPosition()
        {
            var mousePosition = Mouse.current.position.ReadValue();

            var ray = sceneCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, 100f, placementLayerMask)) _lastPosition =  hit.point;

            return _lastPosition;


            //var gridCenter = new Vector3(Mathf.Floor(_lastPosition.x) + 0.5f, _lastPosition.y, Mathf.Floor(_lastPosition.z) + 0.5f);
            //switch(_snapToGrid)
            //{
            //    case false:
            //        return _lastPosition;
            //    case true:
            //        return gridCenter;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(_snapToGrid), _snapToGrid  , null);
            //}
        }
    }
}