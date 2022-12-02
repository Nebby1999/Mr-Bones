using System;
using UnityEditor;
using UnityEngine;

namespace Nebby
{
    [Serializable]
    public class IntReference
    {
        [SerializeField]
        private int constant;
        [SerializeField]
        private IntReferenceAsset assetReference;
        public bool useConstant;

        public IntReference(int value, bool usesConstantValue)
        {
            useConstant = usesConstantValue;
            
            if(useConstant)
            {
                constant = value;
            }
            else
            {
                assetReference = ScriptableObject.CreateInstance<IntReferenceAsset>();
                assetReference.intValue = value;
            }
        }

        public IntReference(IntReference orig)
        {
            useConstant = orig.useConstant;
            constant = orig.constant;
            assetReference = orig.assetReference;
        }

        public int Value
        {
            get
            {
                return useConstant ? constant : assetReference.intValue;
            }
            set
            {
                if(useConstant)
                {
                    constant = value;
                }
                else
                {
                    assetReference.intValue = value;
                }
            }
        }
    }
}