using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public class MultipleFloatReferenceSetter : MonoBehaviour
    {
        [Serializable]
        public class FloatReferencePair
        {
            public FloatReferenceAsset valueToModify;
            public FloatReference newValue;
        }

        public FloatReferencePair[] floatReferencePairs = Array.Empty<FloatReferencePair>();
        public bool destroyOnSet;

        private void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            for(int i = 0; i < floatReferencePairs.Length; i++)
            {
                floatReferencePairs[i].valueToModify.floatValue = floatReferencePairs[i].newValue.Value;
            }
            if (destroyOnSet)
                Destroy(this);
        }
    }
}
