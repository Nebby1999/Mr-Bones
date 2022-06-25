using UnityEngine;
using System.Collections;
using Nebby.CSharpUtils;
using System.Linq;

namespace Nebby.UnityUtils
{
    public class TagContainer : MonoBehaviour
    {
        [SerializeField] private TagObject[] tags;

        public TagObject GetTag(int index)
        {
            return ArrayUtils.GetSafe(ref tags, index);
        }

        public int FindTagIndex(string tagName)
        {
            for(int i = 0; i < tags.Length; i++)
            {
                TagObject tag = tags[i];
                if(tag.name.Equals(tagName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool ObjectHasTag(TagObject tagToCheck)
        {
            return tags.Contains(tagToCheck);
        }

        public bool ObjectHasTag(string tagName)
        {
            int index = FindTagIndex(tagName);
            if (index == -1)
                return false;

            return tags[index].name.Equals(tagName, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}