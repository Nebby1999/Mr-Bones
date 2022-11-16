using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class SetSimulationToNewObject : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particleSystems;

        private GameObject createdObjectInstance;
        private void Awake()
        {
            createdObjectInstance = new GameObject("ParticleSimulation");
            foreach (ParticleSystem system in _particleSystems)
            {
                var main = system.main;
                main.simulationSpace = ParticleSystemSimulationSpace.Custom;
                main.customSimulationSpace = createdObjectInstance.transform;
            }
        }

        private void OnDestroy()
        {
            Destroy(createdObjectInstance);
        }
    }
}
