using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Nebby.UnityUtils;

namespace MrBones
{
    public class PlayerController : MonoBehaviour
    {
        public FloatReference calciumLevel;
        public FloatReference movementSpeed;
        public GameObject shoutIndicatorPrefab;

        /*public CharacterMovementController_OLD CharacterMovementController { get; private set; }
        public ShoutController_OLD ShoutController { get; private set; }*/
        public CharacterMovementController CharacterMovementController { get; private set; }
        public ShoutController ShoutController { get; private set; }

        public Vector2 movementControl;
        public Vector2 lookControl;
        public float fireControl;
        public bool isCharging;

        private void Awake()
        {
            CharacterMovementController = GetComponent<CharacterMovementController>();
            ShoutController = GetComponent<ShoutController>();
        }

        public void FixedUpdate()
        {
            CharacterMovementController.PlayerMovement(movementSpeed.Value * Time.fixedDeltaTime * movementControl);
        }

        public void Update()
        {
            if (shoutIndicatorPrefab)
                UpdateShoutIndicator();

            ShoutController.lookDirection = lookControl;
        }

        public void UpdateShoutIndicator()
        {
            shoutIndicatorPrefab.transform.localPosition = new Vector3(lookControl.x, lookControl.y, shoutIndicatorPrefab.transform.localPosition.z);

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, lookControl);
            shoutIndicatorPrefab.transform.rotation = lookRotation;
        }
        #region Input Related
        public void HandleMovement(InputAction.CallbackContext context)
        {
            movementControl = context.ReadValue<Vector2>();
        }

        public void HandleLook(InputAction.CallbackContext context)
        {
            lookControl = context.ReadValue<Vector2>();
        }

        public void HandleInteraction(InputAction.CallbackContext context)
        {

        }

        public void HandleCharge(InputAction.CallbackContext context)
        {
            isCharging = context.ReadValueAsButton();
        }

        public void HandleFire(InputAction.CallbackContext context)
        {
            fireControl = context.ReadValue<float>();
            ShoutController.HandleShoutProcess(context, isCharging);
        }
        #endregion
    }
}