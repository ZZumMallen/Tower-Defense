using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Partisan
{
    public class GridUpdateHandler : MonoBehaviour
    {
        [SerializeField] private GameObject objectToPlace;
        private InputAction _clickAction;

        private void Awake()
        {
            _clickAction = InputSystem.actions.FindAction("Click");

            if(_clickAction == null)
            {
                Debug.LogError("Click action not found in Input System.", this);
            }
        }

        private void OnEnable()
        {
            _clickAction.performed += OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            var selectedPosition2 = GridMouseData.Instance.GetSelectedMapPosition();
            Instantiate(objectToPlace, selectedPosition2, Quaternion.identity);
        }

        private void Update()
        {
            var selectedPosition = GridMouseData.Instance.GetSelectedMapPosition();
            objectToPlace.transform.position = selectedPosition;

        }

    }
} 