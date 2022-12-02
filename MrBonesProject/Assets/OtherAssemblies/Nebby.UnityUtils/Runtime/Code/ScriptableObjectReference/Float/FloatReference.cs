using System;
using UnityEditor;
using UnityEngine;

namespace Nebby
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField]
        private float constant;
        [SerializeField]
        private FloatReferenceAsset assetReference;
        public bool useConstant;

        public FloatReference(float value, bool usesConstantValue)
        {
            useConstant = usesConstantValue;
            
            if(useConstant)
            {
                constant = value;
            }
            else
            {
                assetReference = ScriptableObject.CreateInstance<FloatReferenceAsset>();
                assetReference.floatValue = value;
            }
        }

        public FloatReference(FloatReference orig)
        {
            useConstant = orig.useConstant;
            constant = orig.constant;
            assetReference = orig.assetReference;
        }

        public float Value
        {
            get
            {
                return useConstant ? constant : assetReference.floatValue;
            }
            set
            {
                if(useConstant)
                {
                    constant = value;
                }
                else
                {
                    assetReference.floatValue = value;
                }
            }
        }
    }
}