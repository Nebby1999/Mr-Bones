using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrBones.Breakable
{
    public interface IBreakable
    {
        public abstract float BreakableHP { get; set; }
        public virtual void TakeDamage(float damage)
        {
            BreakableHP -= damage;
            if (BreakableHP <= 0)
                OnBreak();
        }
        public abstract void OnBreak();
    }
}
