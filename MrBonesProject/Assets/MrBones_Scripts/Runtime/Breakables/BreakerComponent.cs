using Nebby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    public class BreakerComponent : MonoBehaviour
    {
        public bool canHarmBreakable;
        [Tooltip("Convers the rigidbody velocity to the amount of damage to deal to the breakable, 1 Velocitty = 1 velocityToBreakableStrength")]
        public float velocityToBreakableStrength;

        public IBreakablesCollisionCallback[] Callbacks { get; private set; }

        private void Awake()
        {
            Callbacks = GetComponents<IBreakablesCollisionCallback>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!canHarmBreakable)
                return;

            BreakableComponent breakableComponent = collision.gameObject.GetComponentFromRoot<BreakableComponent>();
            
            if(!breakableComponent)
                return;

            BreakablesCollisionInfo callbackInfo = new BreakablesCollisionInfo
            {
                breakerRigidbody = collision.otherRigidbody,
                breakerObject = gameObject.GetRootGameObject(),
                breakerComponent = this,

                breakableRigidbody = collision.rigidbody,
                breakableComponent = breakableComponent,
                breakableObject = breakableComponent.GetRootGameObject(),
            };

            var fatal = breakableComponent.TakeDamage(callbackInfo);
            
            for(int i = 0; i < Callbacks.Length; i++)
            {
                var callback = Callbacks[i];
                callback.OnBreakablesCollision(callbackInfo);
                if(fatal)
                {
                    callback.OnBreakableBroken(callbackInfo);
                }  
            }
        }
    }

    public struct BreakablesCollisionInfo
    {
        public Vector2 BreakerVelocity => breakerRigidbody ? breakerRigidbody.velocity : Vector2.zero;
        public GameObject breakerObject;
        public Rigidbody2D breakerRigidbody;
        public BreakerComponent breakerComponent;
        
        public Vector2 BreakableVelocity => breakableRigidbody ? breakableRigidbody.velocity : Vector2.zero;
        public GameObject breakableObject;
        public Rigidbody2D breakableRigidbody;
        public BreakableComponent breakableComponent;
    }

    public interface IBreakablesCollisionCallback
    {
        public void OnBreakablesCollision(BreakablesCollisionInfo collisionInfo);

        public void OnBreakableBroken(BreakablesCollisionInfo collisionInfoThatBrokeTheBreakable);
    }
}
