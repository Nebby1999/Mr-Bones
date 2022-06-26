using Nebby.UnityUtils;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrBones
{
    public class PlayerInputReceiver : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FloatReference score;

        public float Score
        {
            get => score.Value;
            set => score.Value = value;
        }

        private void Awake()
        {
            playerController.PlayerInputReceiver = this;
        }

        public void OnMove(InputAction.CallbackContext context) => playerController.HandleMovement(context);

        public void OnLook(InputAction.CallbackContext context) => playerController.HandleLook(context);

        public void OnFire(InputAction.CallbackContext context) => playerController.HandleFire(context);

        public void OnCharge(InputAction.CallbackContext context) => playerController.HandleCharge(context);

        public void OnInteract(InputAction.CallbackContext context) => playerController.HandleInteraction(context);
    }
}