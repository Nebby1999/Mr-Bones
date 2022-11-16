using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MrBones;
using Nebby;

namespace EntityStates.MrBones
{
    public class BlastChargeState : MrBonesBaseState
    {
        public static float chargeGain;
        public static AnimationCurve shakeCurve;
        public static string soundDefPoolName;

        private float currentCharge;
        private Vector3 originalLocalPos;
        private Transform spriteTransform;
        private SoundDefPool pool;
        public override void OnEnter()
        {
            base.OnEnter();
            spriteTransform = GetSpriteTransform();
            if(spriteTransform)
            {
                originalLocalPos = spriteTransform.localPosition;
            }
            var spriteBaseTransform = GetSpriteBaseTransform();
            pool = SoundDefPool.FindByCustomName(spriteBaseTransform.gameObject, soundDefPoolName);
            if(pool)
            {
                pool.PlayRandomSound();
            }
            PlayAnimation("Charge");
        }

        public override void Update()
        {
            base.Update();
            Debug.Log(currentCharge);
            float str = shakeCurve.Evaluate(currentCharge);
            var rnd = UnityEngine.Random.insideUnitCircle * str;
            var newPos = new Vector3(originalLocalPos.x + rnd.x, originalLocalPos.y + rnd.y, originalLocalPos.z);

            if(spriteTransform)
            {
                spriteTransform.localPosition = newPos;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var chargeChange = chargeGain * Time.fixedDeltaTime;
            ScreamComponent.CurrentScreamEnergy -= chargeChange;
            currentCharge += chargeChange;

            if(InputSimulator.fire.Canceled || InputSimulator.fire2.Canceled || ScreamComponent.CurrentScreamEnergy == 0)
            {
                var blastState = new BlastState();
                blastState.coefficientStrength = currentCharge;
                outer.SetNextState(blastState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            if(spriteTransform)
            {
                spriteTransform.localPosition = originalLocalPos;
            }
            if(pool)
            {
                pool.StopCurrentSound();
            }
        }
    }
}
