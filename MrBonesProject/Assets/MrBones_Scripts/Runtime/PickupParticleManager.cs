using Nebby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class PickupParticleManager : SingletonBehaviour<PickupParticleManager>
    {
        public ParticleSystem burstSystem;
        public void DoBurst(Color32 burstColor, Vector3 position)
        {
            var instance = Instantiate(burstSystem, position, Quaternion.identity);
            var main = instance.main;
            main.startColor = new ParticleSystem.MinMaxGradient(burstColor);
            instance.Play();
        }
    }
}