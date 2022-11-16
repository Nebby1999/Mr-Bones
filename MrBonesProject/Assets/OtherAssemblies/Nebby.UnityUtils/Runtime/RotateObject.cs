using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nebby
{
    public class RotateObject : MonoBehaviour
    {
        public bool useVector3ForRotation;
        public bool rotateX;
        public bool rotateY;
        public bool rotateZ;
        
        public float speed;
        public Vector3 rotationSpeed;

        // Update is called once per frame
        private void FixedUpdate()
        {
            Vector3 newRotation = GetRotation();

            if(useVector3ForRotation)
            {
                transform.rotation = Quaternion.Euler((transform.eulerAngles + newRotation * Time.fixedDeltaTime));
            }
            else
            {
                transform.rotation = Quaternion.Euler((transform.eulerAngles + (newRotation * speed) * Time.fixedDeltaTime));
            }
        }

        private Vector3 GetRotation()
        {
            if (useVector3ForRotation)
                return rotationSpeed;

            return new Vector3(rotateX ? 1 : 0, rotateY ? 1 : 0, rotateZ ? 1 : 0);
        }
    }
}
