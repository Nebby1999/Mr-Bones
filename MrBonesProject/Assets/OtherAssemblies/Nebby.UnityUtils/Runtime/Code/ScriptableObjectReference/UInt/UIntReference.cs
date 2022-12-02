using System;
using UnityEditor;
using UnityEngine;

namespace Nebby
{
    [Serializable]
    public class UIntReference
    {
        [SerializeField]
        private uint constant;
        [SerializeField]
        private UIntReferenceAsset assetReference;
        public bool useConstant;

        public UIntReference(uint value, bool usesConstantValue)
        {
            useConstant = usesConstantValue;
            
            if(useConstant)
            {
                constant = value;
            }
            else
            {
                assetReference = ScriptableObject.CreateInstance<UIntReferenceAsset>();
                assetReference.uIntValue = value;
            }
        }

        public UIntReference(UIntReference orig)
        {
            useConstant = orig.useConstant;
            constant = orig.constant;
            assetReference = orig.assetReference;
        }

        public uint Value
        {
            get
            {
                return useConstant ? constant : assetReference.uIntValue;
            }
            set
            {
                if(useConstant)
                {
                    constant = value;
                }
                else
                {
                    assetReference.uIntValue = value;
                }
            }
        }
    }
}