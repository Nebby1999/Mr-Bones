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

        private IBreakerCallback[] breakerCallbacks = Array.Empty<IBreakerCallback>();

        private void Awake()
        {
            breakerCallbacks = GetComponentsInChildren<IBreakerCallback>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!canHarmBreakable)
                return;

            IBreakable breakable = collision.gameObject.GetComponent<IBreakable>();
            
            if(breakable == null) //Not a breakable, return.
                return;

            BreakablesCollisionInfo callbackInfo = new BreakablesCollisionInfo
            {
                collision = collision,
                breakerRigidbody = collision.otherRigidbody,
                breakerObject = gameObject.GetRootGameObject(),
                breakerComponent = this,

                breakableRigidbody = collision.rigidbody,
                breakableComponent = breakable,
                breakableObject = ((Component)breakable).gameObject,
            };

            var fatal = breakable.TakeDamage(callbackInfo);
            for(int i = 0; i < breakerCallbacks.Length; i++)
            {
                IBreakerCallback callback = breakerCallbacks[i];
                if(fatal)
                {
                    callback.OnBreakableBroken(callbackInfo);
                    continue;
                }
                callback.OnBreakableCollision(callbackInfo);
            }
        }
    }

    public struct BreakablesCollisionInfo
    {
        public Collision2D collision;
        public Vector2 BreakerVelocity => breakerRigidbody ? breakerRigidbody.velocity : Vector2.zero;
        public GameObject breakerObject;
        public Rigidbody2D breakerRigidbody;
        public BreakerComponent breakerComponent;
        
        public Vector2 BreakableVelocity => breakableRigidbody ? breakableRigidbody.velocity : Vector2.zero;
        public GameObject breakableObject;
        public Rigidbody2D breakableRigidbody;
        public IBreakable breakableComponent;
    }

    public interface IBreakerCallback //Callback for the breaker
    {
        public void OnBreakableCollision(BreakablesCollisionInfo collisionInfo)
        {
        }

        public void OnBreakableBroken(BreakablesCollisionInfo collisionInfoThatBrokeTheBreakable)
        {
        }
    }
}
