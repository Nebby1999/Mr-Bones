using Nebby.UnityUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MrBones.Breakable
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BreakableBehaviour : MonoBehaviour
    {
        public FloatReference health;
        public FloatReference maxHealth;
        public UnityEvent OnBreakEvent;

        protected virtual void Awake()
        {
            maxHealth.Value = health.Value;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            var colliderGO = collision.collider.gameObject;
            var breakableBreaker = colliderGO.GetComponent<IBreakableBreaker>();
            if (breakableBreaker == null)
                return;

            health.Value -= breakableBreaker.DealDamageToBreakable(this, gameObject.GetRootGameObject());

            if(health.Value <= 0)
            {
                OnBreak();
                Destroy(gameObject);
            }
        }

        protected virtual void OnBreak()
        {
            OnBreakEvent.Invoke();
        }
    }
}
