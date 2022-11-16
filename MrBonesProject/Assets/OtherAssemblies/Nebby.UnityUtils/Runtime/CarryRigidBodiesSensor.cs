using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    [RequireComponent(typeof(Collider))]
    public class CarryRigidBodiesSensor : MonoBehaviour
    {
        public CarryRigidBodies carrier;

        public void OnTriggerEnter(Collider other)
        {
            if(carrier.useSensorAsTrigger)
            {
                Rigidbody rb = other.GetComponentFromRoot<Rigidbody>();
                if (rb)
                    carrier.AddRigidbody(rb);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if(carrier.useSensorAsTrigger)
            {
                Rigidbody rb = other.GetComponentFromRoot<Rigidbody>();
                if (rb)
                    carrier.RemoveRigidbody(rb);

            }
        }
    }
}
