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

        public ParticleSystem shoutParticles;
        public States currentState;
        public CharacterMovementController charMovementController;


        public float strength;
        public float jetpackStrength;
        public float chargeStrength;
        public float maxChargeStrength;
        public Vector2 lookDirection;

        private void Awake()
        {
            if (!shoutParticles)
                shoutParticles.GetComponent<ParticleSystem>();
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
            shoutParticles.transform.rotation = lookRotation;

            var emission = shoutParticles.emission;
            emission.rateOverTime = strength * 10;
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
            charMovementController.JetpackBoost(lookDirection, strength * jetpackStrength);
        }

        private void ChargeState()
        {

        }
    }

}