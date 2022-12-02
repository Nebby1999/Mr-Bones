using System.Collections;
using UnityEngine;

namespace Nebby
{
    public class FloatReferenceSetter : MonoBehaviour
    {
        public FloatReference newValue;
        public FloatReferenceAsset valueToModify;
        public bool destroyOnSet;

        public void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            valueToModify.floatValue = newValue.Value;
            if (destroyOnSet)
                Destroy(this);
        }
    }
}