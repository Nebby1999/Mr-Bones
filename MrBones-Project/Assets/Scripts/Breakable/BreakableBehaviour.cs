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
    public class BreakableBehaviour : MonoBehaviour, IBreakable
    {
        public float BreakableHP { get => health.Value; set => health.Value = value; }
        public FloatReference health;
        public void OnBreak()
        {
            throw new NotImplementedException();
        }
    }
}
