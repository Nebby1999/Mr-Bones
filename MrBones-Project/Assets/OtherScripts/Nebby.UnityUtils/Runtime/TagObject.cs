using UnityEngine;
using System.Collections;

namespace Nebby.UnityUtils
{
    [CreateAssetMenu(menuName = "Nebby/Tag")]
    public class TagObject : ScriptableObject
    {
        public override bool Equals(object other)
        {
            if(other is TagObject tagObj)
            {
                return this.name.Equals(tagObj.name, System.StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
