using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public class MultipleUIntReferenceSetter : MonoBehaviour
    {
        [Serializable]
        public class UIntReferencePair
        {
            public UIntReferenceAsset valueToModify;
            public UIntReference newValue;
        }

        public UIntReferencePair[] intReferencePairs = Array.Empty<UIntReferencePair>();
        public bool destroyOnSet;

        private void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            for(int i = 0; i < intReferencePairs.Length; i++)
            {
                intReferencePairs[i].valueToModify.uIntValue = intReferencePairs[i].newValue.Value;
            }
            if (destroyOnSet)
                Destroy(this);
        }
    }
}
