using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MrBones;
using Nebby;

namespace EntityStates
{
    public abstract class EntityState : EntityStateBase
    {
        public new EntityStateMachine outer => base.outer as EntityStateMachine;
        public CharacterBody Body => outer.CommonComponents.characterBody;
        public Rigidbody2D RigidBody => outer.CommonComponents.rigidbody;
        public InputSimulator InputSimulator => outer.CommonComponents.inputSimulator;
        public Transform Transform => outer.CommonComponents.transform;
        public SpriteLocator SpriteLocator => outer.CommonComponents.spriteLocator;
        public ChildLocator ChildLocator => outer.CommonComponents.childLocator;
        public CharacterSoundDefManager CharacterSoundDefManager => outer.CommonComponents.characterSoundDefManager;

        protected Transform GetSpriteBaseTransform()
        {
            return SpriteLocator ? SpriteLocator.spriteBaseTransform : null;
        }

        protected Transform GetSpriteTransform()
        {
            return SpriteLocator ? SpriteLocator._spriteTransform : null;
        }

        protected Animator GetSpriteAnimator()
        {
            if(SpriteLocator && SpriteLocator._spriteTransform)
            {
                return SpriteLocator._spriteTransform.GetComponent<Animator>();
            }
            return null;
        }

        protected void PlayAnimation(string animationStateName, string playbackRateParam, float duration)
        {
            Animator spriteAnimator = GetSpriteAnimator();
            PlayAnimationOnAnimator(spriteAnimator, animationStateName, playbackRateParam, duration);
        }

        protected void PlayAnimationOnAnimator(Animator spriteAnimator, string animationStateName, string playbackRateParam, float duration )
        {
            spriteAnimator.speed = 1f;
            spriteAnimator.Update(0);

            spriteAnimator.SetFloat(playbackRateParam, 1f);
            spriteAnimator.PlayInFixedTime(animationStateName, 0, 0);
            spriteAnimator.Update(0);
            float length = spriteAnimator.GetCurrentAnimatorStateInfo(0).length;
            spriteAnimator.SetFloat(playbackRateParam, length / duration);
        }

        protected virtual void PlayAnimation(string animationStateName)
        {
            Animator spriteAnimator = GetSpriteAnimator();
            if(spriteAnimator)
            {
                PlayAnimationOnAnimator(spriteAnimator, animationStateName);
            }
        }

        protected static void PlayAnimationOnAnimator(Animator spriteAnimator, string animationStateName)
        {
            spriteAnimator.speed = 1f;
            spriteAnimator.Update(0);
            spriteAnimator.PlayInFixedTime(animationStateName, 0, 0);
        }

        protected void PlaySoundOnSoundManager(SoundDef sound)
        {
            if(CharacterSoundDefManager)
            {
                CharacterSoundDefManager.PlaySound(sound);
            }
        }
    }
}
