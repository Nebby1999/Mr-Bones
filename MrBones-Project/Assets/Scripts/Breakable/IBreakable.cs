using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrBones.Breakable
{
    public interface IBreakableBreaker
    {
        public abstract float DealDamageToBreakable(BreakableBehaviour behaviour, GameObject rootGameObject);
    }
}
