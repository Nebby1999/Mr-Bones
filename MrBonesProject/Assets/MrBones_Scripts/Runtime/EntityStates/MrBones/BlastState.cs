using Nebby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MrBones;

namespace EntityStates.MrBones
{
    public class BlastState : MrBonesBaseState
    {
        public static float blastStrength;
        public static float blastDuration;
        public static SoundDef blastSound;

        public float coefficientStrength;
        private float blastTimer;
        private ParticleSystem burst;
        private BreakerComponent breakerComponent;
        public override void OnEnter()
        {
            base.OnEnter();

            breakerComponent = GetComponent<BreakerComponent>();
            if (breakerComponent)
                breakerComponent.canHarmBreakable = true;

            var lookDirection = InputSimulator.AimDirection;
            if (lookDirection == Vector2.zero)
                lookDirection = Vector2.up;

            MrBonesMovement.Burst(lookDirection, blastStrength * coefficientStrength);
            PlayAnimation("Blast");

            PlaySoundOnSoundManager(blastSound);
            var burstObj = ChildLocator.FindChild("Burst");
            if(burstObj)
            {
                Quaternion lookRot = Quaternion.LookRotation(Vector3.forward, InputSimulator.AimInput);
                burstObj.rotation = lookRot;

                burst = burstObj.GetComponent<ParticleSystem>();
                burst.Play();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(FixedAge > blastDuration)
            {
                outer.SetNextStateToMain();
            }
        }

        public override void OnExit()
        {
            if(breakerComponent)
                breakerComponent.canHarmBreakable = false;

            base.OnExit();
        }
    }
}
