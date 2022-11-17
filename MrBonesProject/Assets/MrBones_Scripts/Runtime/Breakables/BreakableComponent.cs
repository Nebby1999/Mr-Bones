using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MrBones
{
    public class BreakableComponent : MonoBehaviour
    {
        public float MaxResistance => _resistance;
        [SerializeField]private float _resistance;
        public float CurrentResistance => _currentResistance;
        private float _currentResistance;

        public UnityEvent onBreak;

        public IBreakablesCollisionCallback[] Callbacks { get; private set; }

        private void Awake()
        {
            Callbacks = GetComponents<IBreakablesCollisionCallback>();
            _currentResistance = MaxResistance;
        }

        public bool TakeDamage(BreakablesCollisionInfo info)
        {
            var velocityOfBreaker = info.BreakerVelocity;
            var damageAsVector = info.breakerComponent.velocityToBreakableStrength * velocityOfBreaker;
            var actualDamage = (Mathf.Abs(damageAsVector.x) + Mathf.Abs(damageAsVector.y)) / 2;

            _currentResistance -= actualDamage;

            for(int i = 0; i < Callbacks.Length; i++)
            {
                Callbacks[i].OnBreakablesCollision(info);
            }

            if (_currentResistance <= 0)
            {
                Break(info);
                return true;
            }
            return false;
        }

        private void Break(BreakablesCollisionInfo info)
        {
            onBreak?.Invoke();
            for(int i = 0; i < Callbacks.Length; i++)
            {
                Callbacks[i].OnBreakableBroken(info);
            }
            Destroy(gameObject);
        }
    }
}
