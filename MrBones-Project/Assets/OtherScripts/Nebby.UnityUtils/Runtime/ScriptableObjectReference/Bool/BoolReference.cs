using System;
using UnityEditor;
using UnityEngine;

namespace Nebby.UnityUtils
{
    [Serializable]
    public class BoolReference
    {
        [SerializeField]
        private bool constant;
        [SerializeField]
        private BoolReferenceAsset assetReference;
        public bool useConstant;

        public BoolReference(bool value, bool usesConstantValue)
        {
            useConstant = usesConstantValue;
            
            if(useConstant)
            {
                constant = value;
            }
            else
            {
                assetReference = ScriptableObject.CreateInstance<BoolReferenceAsset>();
                assetReference.boolValue = value;
            }
        }
        public bool Value
        {
            get
            {
                return useConstant ? constant : assetReference.boolValue;
            }
            set
            {
                if(useConstant)
                {
                    constant = value;
                }
                else
                {
                    assetReference.boolValue = value;
                }
            }
        }
    }
}