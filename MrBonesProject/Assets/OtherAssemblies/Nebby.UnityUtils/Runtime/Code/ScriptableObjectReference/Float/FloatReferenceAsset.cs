using System.Collections;
using UnityEngine;

namespace Nebby
{
    [CreateAssetMenu(menuName = "Nebby/ValueReferences/FloatReference", fileName = "New FloatReference")]
    public class FloatReferenceAsset : ScriptableObject
    {
        public float floatValue;
    }
}