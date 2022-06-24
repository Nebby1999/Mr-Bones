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

        public ShoutParticleController shoutParticleController;
        public States currentState;
        public CharacterMovementController charMovementController;
        public float jetpackCoefficient;
        public float maxChargeStrength;
        public UnityEvent OnBurst;
        public UnityEvent OnConstantScreamStart;
        public UnityEvent OnConstantScreamEnd;
        public Vector2 LookDirection { get; set; }

        private float strength;
        private float chargeStrength;

        private void Update()
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, LookDirection);
            shoutParticleController.transform.rotation = lookRotation;
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
            if(currentState == States.Jetpack && callbackContext.started)
            {
                OnConstantScreamStart?.Invoke();
            }
        }

        private void JetpackState()
        {
            charMovementController.JetpackBoost(LookDirection, strength * jetpackCoefficient);
        }

        private void ChargeState()
        {
            chargeStrength += Time.fixedDeltaTime * 20;
            if (chargeStrength > maxChargeStrength)
                chargeStrength = maxChargeStrength;
        }

        private void Burst()
        {
            currentState = States.Idle;
            charMovementController.Burst(LookDirection, chargeStrength);
            chargeStrength = 0;
            OnBurst?.Invoke();
        }
    }

}