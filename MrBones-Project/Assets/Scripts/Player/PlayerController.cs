using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Nebby.UnityUtils;
using MrBones.Pickups;
using UnityEngine.Events;
using MrBones.Breakable;

namespace MrBones
{
    public class PlayerController : MonoBehaviour, IPickupCollector, IBreakableBreaker
    {
        [Header("Calcium Related")]
        public FloatReference calciumLevel;
        public FloatReference maxCalcium;
        public FloatReference calciumLossPerSecond;
        public FloatReference calciumLossCoefficient;
        public UnityEvent OnDeath;
        public bool Alive { get => calciumLevel.Value > 0; }
        private bool wasAlive = true;

        [Header("Other")]
        public FloatReference movementSpeed;
        public PointerController pointerController;
        public GameObject jawPrefab;

        public CharacterMovementController CharacterMovementController { get; private set; }
        public MrBonesAnimatorController AnimatorController { get; private set; }
        public ShoutController ShoutController { get; private set; }
        public PlayerInputReceiver PlayerInputReceiver { get; set; }

        private Vector2 movementControl;
        private Vector2 lookControl;
        private float fireControl;
        private bool isCharging;

        private void Awake()
        {
            CharacterMovementController = GetComponent<CharacterMovementController>();
            ShoutController = GetComponent<ShoutController>();
            AnimatorController = GetComponent<MrBonesAnimatorController>();
        }

        public void FixedUpdate()
        {
            CharacterMovementController.PlayerMovement(movementSpeed.Value * Time.fixedDeltaTime * movementControl);
            calciumLevel.Value -= calciumLossPerSecond.Value * calciumLossCoefficient.Value * Time.fixedDeltaTime;
        }

        public void Update()
        {
            pointerController.LookDirection = lookControl;
            ShoutController.LookDirection = lookControl;
            AnimatorController.ScreamParam = fireControl;
            AnimatorController.IsCharging = ShoutController.currentState == ShoutController.States.Charge;

            AnimatorController.Dead = !Alive;
            if(!Alive && wasAlive)
            {
                wasAlive = false;
                OnPlayerDeath();
            }
        }

        public bool OnPickupInteraction(GameObject rootGO, PickupDef pickupDef)
        {
            switch(pickupDef)
            {
                case CalciumPickup calcium:
                    {
                        calciumLevel.Value += calcium.calciumAmount.Value;
                        if (calciumLevel.Value > maxCalcium.Value)
                            maxCalcium.Value = calciumLevel.Value;
                        return true;
                    }
                case GemPickup gem:
                    {
                        PlayerInputReceiver.Score += gem.score.Value;
                        return true;
                    }
            }
            return false;
        }

        public float DealDamageToBreakable(BreakableBehaviour behaviour, GameObject rootGameObject)
        {
            if(ShoutController.JustBursted)
            {
                return ShoutController.PreviousChargeStrength;
            }
            return 0;
        }

        public void OnBurst(float shoutStrength)
        {
            var toDeduct = shoutStrength / 10;
            calciumLevel.Value -= toDeduct;
        }

        private void OnPlayerDeath()
        {
            Instantiate(jawPrefab, transform.position, transform.rotation);
            OnDeath?.Invoke();
        }
        #region Input Related
        public void HandleMovement(InputAction.CallbackContext context)
        {
            if (!Alive)
                return;
            movementControl = context.ReadValue<Vector2>();
        }

        public void HandleLook(InputAction.CallbackContext context)
        {
            if (!Alive)
                return;
            lookControl = context.ReadValue<Vector2>();
        }

        public void HandleInteraction(InputAction.CallbackContext context)
        {
            if (!Alive)
                return;
        }

        public void HandleCharge(InputAction.CallbackContext context)
        {
            if (!Alive)
                return;
            isCharging = context.ReadValueAsButton();
        }

        public void HandleFire(InputAction.CallbackContext context)
        {
            if (!Alive)
                return;
            fireControl = context.ReadValue<float>();
            calciumLossCoefficient.Value = 1 + fireControl;
            ShoutController.HandleShoutProcess(context, isCharging);
        }
        #endregion
    }
}
