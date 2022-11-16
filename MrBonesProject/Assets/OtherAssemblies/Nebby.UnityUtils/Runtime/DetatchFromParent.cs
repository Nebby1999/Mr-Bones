using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public class DetatchFromParent : MonoBehaviour
    {
        public Transform parentObject;
        public bool parentIsRoot;
        public void Awake()
        {
            if (!parentObject)
                parentObject = parentIsRoot ? transform.root : transform.parent;

            transform.parent = null;
        }
    }
}
