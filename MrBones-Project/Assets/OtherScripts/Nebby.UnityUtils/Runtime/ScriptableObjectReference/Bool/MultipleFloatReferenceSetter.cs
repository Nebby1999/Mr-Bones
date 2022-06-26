using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby.UnityUtils
{
    public class MultipleBoolReferenceSetter : MonoBehaviour
    {
        [Serializable]
        public class BoolReferencePair
        {
            public BoolReferenceAsset valueToModify;
            public BoolReference newValue;
        }

        public BoolReferencePair[] floatReferencePairs = Array.Empty<BoolReferencePair>();
        public bool destroyOnSet;

        private void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            for(int i = 0; i < floatReferencePairs.Length; i++)
            {
                floatReferencePairs[i].valueToModify.boolValue = floatReferencePairs[i].newValue.Value;
            }
            if (destroyOnSet)
                Destroy(this);
        }
    }
}
