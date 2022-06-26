using Nebby.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MrBones
{
    public class ShoutController : MonoBehaviour
    {
        public enum States
        {
            Idle,
            Jetpack,
            Charge,
            Burst
        }

        public States currentState;
        public ShoutParticleController shoutParticleController;
        public CharacterMovementController charMovementController;
        public float jetpackCoefficient;
        [SerializeField] private FloatReference chargeStrength;
        public FloatReference maxChargeStrength;
        public UnityEvent OnChargeStart;
        public UnityEvent<float> OnBurst;
        public UnityEvent OnConstantScreamStart;
        public UnityEvent OnConstantScreamEnd;
        public Vector2 LookDirection { get; set; }
        public float ChargeStrength { get => chargeStrength.Value; }
        public float PreviousChargeStrength { get; private set; }
        public bool JustBursted { get; private set; }
        private float burstTimer;

        private float strength;

        private void Update()
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, LookDirection);
            shoutParticleController.transform.rotation = lookRotation;
            UpdateJustBurstedTimer();
        }

        private void UpdateJustBurstedTimer()
        {
            if(JustBursted)
            {
                if (burstTimer <= 0)
                {
                    JustBursted = false;
                    burstTimer = 0;
                }
                burstTimer -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            switch(currentState)
            {
                case States.Jetpack:
                    JetpackState();
                    break;
                case States.Charge:
                    ChargeState();
                    break;
                case States.Burst:
                    Burst();
                    break;
            }
        }
        public void HandleShoutProcess(InputAction.CallbackContext callbackContext, bool isCharging)
        {
            strength = callbackContext.ReadValue<float>();

            bool isChargingOrInChargeState = (isCharging || currentState == States.Charge);

            if(!isChargingOrInChargeState)
                shoutParticleController.strengthEmission = strength;
            
            if (callbackContext.canceled)
            {
                if (currentState == States.Jetpack)
                    OnConstantScreamEnd?.Invoke();
                currentState = isChargingOrInChargeState ? States.Burst : States.Idle;
                return;
            }


            currentState = isChargingOrInChargeState ? States.Charge : States.Jetpack;
            if(callbackContext.started)
            {
                switch(currentState)
                {
                    case States.Jetpack:
                        OnConstantScreamStart?.Invoke();
                        break;
                    case States.Charge:
                        OnChargeStart?.Invoke();
                        break;
                }
            }
        }

        private void JetpackState()
        {
            charMovementController.JetpackBoost(LookDirection, strength * jetpackCoefficient);
        }

        private void ChargeState()
        {
            chargeStrength.Value += Time.fixedDeltaTime * 20;
            if (chargeStrength.Value > maxChargeStrength.Value)
                chargeStrength.Value = maxChargeStrength.Value;
        }

        private void Burst()
        {
            PreviousChargeStrength = chargeStrength.Value;
            currentState = States.Idle;
            charMovementController.Burst(LookDirection, chargeStrength.Value);
            chargeStrength.Value = 0;
            JustBursted = true;
            burstTimer = 1;
            OnBurst?.Invoke(PreviousChargeStrength);
        }
    }

}