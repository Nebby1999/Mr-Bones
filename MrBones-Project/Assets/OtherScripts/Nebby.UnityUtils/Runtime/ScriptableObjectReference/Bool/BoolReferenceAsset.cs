using System.Collections;
using UnityEngine;

namespace Nebby.UnityUtils
{
    [CreateAssetMenu(menuName = "Nebby/ValueReferences/BoolReference", fileName = "New BoolReference")]
    public class BoolReferenceAsset : ScriptableObject
    {
        public bool boolValue;
    }
}