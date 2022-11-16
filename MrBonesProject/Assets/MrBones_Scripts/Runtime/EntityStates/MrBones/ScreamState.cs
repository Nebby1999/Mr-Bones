using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nebby;
using MrBones;
using UnityEngine;

namespace EntityStates.MrBones
{
    public class ScreamState : MrBonesBaseState
    {
        public static float screamEnergyDeductedPerSecond;
        public static float screamForce;
        public static float fallingScreamForceCoefficient;
        public static float minParticleEmissionRate;
        public static float maxParticleEmissionRate;
        public static string screamStateName;
        public static string screamPlaybackRateName;
        public static string soundDefPoolName;


        private Animator spriteAnimator;
        private ParticleSystem jetpack;
        private ParticleSystem.EmissionModule jetpackEmission;
        private SoundDefPool pool;
        public override void OnEnter()
        {
            base.OnEnter();

            spriteAnimator = GetSpriteAnimator();
            PlayAnimation(screamStateName);
            var jetpackObj = ChildLocator.FindChild("Jetpack");
            if(jetpackObj)
            {
                jetpack = jetpackObj.GetComponent<ParticleSystem>();
                jetpackEmission = jetpack.emission;
            }
            var spriteBaseTransform = GetSpriteBaseTransform();
            pool = SoundDefPool.FindByCustomName(spriteBaseTransform.gameObject, soundDefPoolName);
            if (pool)
            {
                pool.PlayRandomSound();
            }
        }

        public override void Update()
        {
            base.Update();
            if(jetpack)
            {
                Quaternion lookRot = Quaternion.LookRotation(Vector3.forward, InputSimulator.AimInput);
                jetpack.transform.rotation = lookRot;

                jetpackEmission.rateOverTime = Mathf.Lerp(minParticleEmissionRate, maxParticleEmissionRate, InputSimulator.fireInput);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            spriteAnimator.SetFloat(screamPlaybackRateName, InputSimulator.fireInput);
            DeductScreamEnergy();
            if(InputSimulator.fire.Canceled || !CanScream)
            {
                outer.SetNextStateToMain();
            }

            var lookDirection = InputSimulator.AimDirection;
            if(lookDirection != Vector2.zero)
            {
                var force = screamForce;
                Vector2 rbVelocity = RigidBody.velocity;

                if (rbVelocity.y < 0 && lookDirection.y < -0.75)
                    force *= fallingScreamForceCoefficient;

                MrBonesMovement.DoJetpackBoost(-lookDirection, force);
            }
        }

        private void DeductScreamEnergy()
        {
            ScreamComponent.CurrentScreamEnergy -= screamEnergyDeductedPerSecond * Time.fixedDeltaTime;
        }

        public override void OnExit()
        {
            base.OnExit();
            jetpackEmission.rateOverTime = 0;
            if(pool)
            {
                pool.StopCurrentSound();
            }
        }
    }
}
