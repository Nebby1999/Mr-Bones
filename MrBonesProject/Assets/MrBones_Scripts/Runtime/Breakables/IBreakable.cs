using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public interface IBreakable //Represents something that can be broken by a breaker
    {
        public abstract bool TakeDamage(BreakablesCollisionInfo info);
    }

    /// <summary>
    /// Callback for when a breakable collides with a breaker, or when it breaks
    /// <para>May or may not be called by IBreakable, as the calling is implemented in a case by case basis</para>
    /// </summary>
    public interface IBreakableCallback //Callback for other effects
    {
        public void OnBreakerCollision(BreakablesCollisionInfo collisionInfo)
        {
        }

        public void OnBreakableBroken(BreakablesCollisionInfo collisionInfoThatBrokeTheBreakable)
        {
        }
    }
}