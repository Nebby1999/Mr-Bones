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

        public CharacterMovementController CharacterMovementController { get; private set; }
        public ShoutController ShoutController { get; private set; }

        private Vector2 movementControl;
        public Vector2 lookControl;
        public float fireControl;

        private void Awake()
        {
            CharacterMovementController = GetComponent<CharacterMovementController>();
            ShoutController = GetComponent<ShoutController>();
        }
        public void FixedUpdate()
        {
            CharacterMovementController.NewMove(movementSpeed.Value * Time.fixedDeltaTime * movementControl);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, ((Vector2)transform.position) + lookControl);
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

        public void HandleFire(InputAction.CallbackContext context)
        {
            ShoutController.HandleShoutProcess(context, lookControl);
        }
        #endregion
    }
}
