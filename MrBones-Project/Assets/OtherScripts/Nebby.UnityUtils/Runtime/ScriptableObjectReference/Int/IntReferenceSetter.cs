using System.Collections;
using UnityEngine;

namespace Nebby.UnityUtils
{
    public class IntReferenceSetter : MonoBehaviour
    {
        public IntReference newValue;
        public IntReferenceAsset valueToModify;
        public bool destroyOnSet;

        public void Start()
        {
            SetValue();
        }

        public void SetValue()
        {
            valueToModify.intValue = newValue.Value;
            if (destroyOnSet)
                Destroy(this);
        }
    }
}