using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class ShoutParticleController : MonoBehaviour
    {
        public ParticleSystem jetpack;
        public ParticleSystem burst;

        public float strengthEmission;
        public void Update()
        {
            var emission = jetpack.emission;
            emission.rateOverTime = strengthEmission * 10;
        }
        public void DoBurst()
        {
            burst.Play();
        }
    }
}
