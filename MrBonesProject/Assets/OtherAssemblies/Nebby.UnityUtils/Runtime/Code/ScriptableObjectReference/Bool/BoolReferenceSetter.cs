using System.Collections;
using UnityEngine;

namespace Nebby
{
    public class BoolReferenceSetter : MonoBehaviour
    {
        public BoolReference newValue;
        public BoolReferenceAsset valueToModify;
        public bool destroyOnSet;

        public void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            valueToModify.boolValue = newValue.Value;
            if (destroyOnSet)
                Destroy(this);
        }
    }
}