using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public class CarryRigidBodies : MonoBehaviour
    {
        public bool useSensorAsTrigger = true;
        public List<Rigidbody> rigidbodies = new List<Rigidbody>();
        public Rigidbody thisRigidBody;
        public Vector3 lastPosition;

        private void Awake()
        {
            lastPosition = transform.position;
        }

        private void LateUpdate()
        {
            Vector3 velocity = (transform.position - lastPosition);
            foreach(Rigidbody rb in rigidbodies)
            {
                rb.MovePosition(rb.transform.position + velocity);
            }

            lastPosition = transform.position;
        }
        private void OnCollisionEnter(Collision c)
        {
            Rigidbody rb = c.collider.GetComponent<Rigidbody>();
            if (rb)
                AddRigidbody(rb);
        }

        private void OnCollisionExit(Collision c)
        {
            Rigidbody rb = c.collider.GetComponent<Rigidbody>();
            if (rb)
                RemoveRigidbody(rb);
        }

        public void AddRigidbody(Rigidbody instance)
        {
            if(!rigidbodies.Contains(instance) && instance != thisRigidBody)
            {
                rigidbodies.Add(instance);
            }
        }

        public void RemoveRigidbody(Rigidbody instance)
        {
            if(rigidbodies.Contains(instance) && instance != thisRigidBody)
            {
                rigidbodies.Remove(instance);
            }
        }
    }
}