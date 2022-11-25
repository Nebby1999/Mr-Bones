using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace MrBones
{
    public class BreakableBox : MonoBehaviour, IBreakable
    {
        public float MaxHealth => _resistance;
        [SerializeField]private float _resistance;

        public float Health => _currentResistance;
        private float _currentResistance;


        public UnityEvent onBreak;


        private void Awake()
        {
            _currentResistance = MaxHealth;
        }

        public bool TakeDamage(BreakablesCollisionInfo info)
        {
            var velocityOfBreaker = info.BreakerVelocity;
            var damageAsVector = info.breakerComponent.velocityToBreakableStrength * velocityOfBreaker;
            var actualDamage = (Mathf.Abs(damageAsVector.x) + Mathf.Abs(damageAsVector.y)) / 2;

            _currentResistance -= actualDamage;

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
            Destroy(gameObject);
        }
    }
}
