using System.Collections;
using UnityEngine;

namespace Nebby
{
    [CreateAssetMenu(menuName = "Nebby/ValueReferences/UIntReference", fileName = "New UIntReference")]
    public class UIntReferenceAsset : ScriptableObject
    {
        public uint uIntValue;
    }
}