using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrBones
{
    public class ShoutController : MonoBehaviour
    {
        public enum States
        {
            Idle,
            Jetpack,
            Charge
        }

        public ShoutParticleController shoutParticleController;
        public States currentState;
        public CharacterMovementController charMovementController;


        public float strength;
        public float jetpackCoefficient;
        public float chargeStrength;
        public float maxChargeStrength;
        public Vector2 lookDirection;

        private void Awake()
        {
        }

        public void Update()
        {
            UpdateParticleSystem();
        }
        private void FixedUpdate()
        {
            switch(currentState)
            {
                case States.Jetpack:
                    JetpackState();
                    break;
            }
        }

        private void UpdateParticleSystem()
        {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            shoutParticleController.transform.rotation = lookRotation;

            switch(currentState)
            {
                case States.Jetpack:
                    JetpackUpdate(shoutParticleController.jetpackSystem);
                    break;
                case States.Charge:
                    ChargeUpdate(shoutParticleController);
                    break;
            }
            var emission = shoutParticleController.emission;
            emission.rateOverTime = strength * 10;
        }

        private void JetpackUpdate()
        {

        }
        public void HandleShoutProcess(InputAction.CallbackContext callbackContext, bool isCharging)
        {
            strength = callbackContext.ReadValue<float>();

            if (callbackContext.canceled)
            {
                currentState = States.Idle;
                return;
            }


            currentState = isCharging ? States.Charge : States.Jetpack;
        }

        private void JetpackState()
        {
            charMovementController.JetpackBoost(lookDirection, strength * jetpackCoefficient);
        }

        private void ChargeState()
        {

        }
    }

}