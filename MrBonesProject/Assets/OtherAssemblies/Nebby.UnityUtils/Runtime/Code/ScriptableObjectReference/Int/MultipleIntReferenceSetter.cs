using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public class MultipleIntReferenceSetter : MonoBehaviour
    {
        [Serializable]
        public class IntReferencePair
        {
            public IntReferenceAsset valueToModify;
            public IntReference newValue;
        }

        public IntReferencePair[] intReferencePairs = Array.Empty<IntReferencePair>();
        public bool destroyOnSet;

        private void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            for(int i = 0; i < intReferencePairs.Length; i++)
            {
                intReferencePairs[i].valueToModify.intValue = intReferencePairs[i].newValue.Value;
            }
            if (destroyOnSet)
                Destroy(this);
        }
    }
}
