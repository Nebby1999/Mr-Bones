using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EntityStates.MrBones
{
    public class Idle : MrBonesBaseState
    {
        public static float screamEnergyRestoredPerSecond;
        public static float onAirRestoringCoefficient;
        public static float maxAnimationSpeedCoef;
        public static float minAnimationSpeedCoef;
        public static string animationName;
        public static string animationPlaybackRateParam;

        private float idleTimer;
        private float idleStopwatch;
        public override void OnEnter()
        {
            base.OnEnter();
            PlayIdleAnimation();
        }

        public override void Update()
        {
            base.Update();
            idleStopwatch += Time.deltaTime;
            if(idleStopwatch > idleTimer)
            {
                PlayIdleAnimation();
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            var lookDir = InputSimulator.AimDirection;
            if(lookDir != Vector2.zero && InputSimulator.fire.Canceled)
                RestoreScream();
    
            if (CanScream && InputSimulator.fire.Performed)
            {
                if(InputSimulator.fire2.Performed)
                {
                    outer.SetNextState(new BlastChargeState());
                    return;
                }

                outer.SetNextState(new ScreamState());
            }
        }

        private void RestoreScream()
        {
            var restorePerSecond = screamEnergyRestoredPerSecond;
            ScreamComponent.CurrentScreamEnergy += screamEnergyRestoredPerSecond * Time.fixedDeltaTime;
        }

        private void PlayIdleAnimation()
        {
            Animator animator = GetSpriteAnimator();
            float num = Random.Range(minAnimationSpeedCoef, maxAnimationSpeedCoef);
            PlayAnimation(animationName, animationPlaybackRateParam, num);

            idleTimer = animator.GetCurrentAnimatorStateInfo(0).length * num;
            idleStopwatch = 0;
        }
    }
}
