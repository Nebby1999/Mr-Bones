using System.Collections;
using UnityEngine;

namespace Nebby
{
    [CreateAssetMenu(menuName = "Nebby/ValueReferences/IntReference", fileName = "New IntReference")]
    public class IntReferenceAsset : ScriptableObject
    {
        public int intValue;
    }
}