using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby;

namespace MrBones
{
    public class EntityStateMachine : EntityStateMachineBase
    {
        public class CommonComponentLocator
        {
            public readonly CharacterBody characterBody;
            public readonly InputSimulator inputSimulator;
            public readonly Rigidbody2D rigidbody;
            public readonly Transform transform;
            public readonly SpriteLocator spriteLocator;
            public readonly ChildLocator childLocator;
            public readonly CharacterSoundDefManager characterSoundDefManager;

            public CommonComponentLocator(GameObject go)
            {
                characterBody = go.GetComponent<CharacterBody>();
                rigidbody = go.GetComponent<Rigidbody2D>();
                inputSimulator = go.GetComponent<InputSimulator>();
                transform = go.transform;
                spriteLocator = go.GetComponent<SpriteLocator>();
                childLocator = go.GetComponent<ChildLocator>();
                characterSoundDefManager = go.GetComponent<CharacterSoundDefManager>();
            }
        }

        public CommonComponentLocator CommonComponents { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CommonComponents = new CommonComponentLocator(gameObject);
        }
    }
}
