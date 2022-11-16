using System.Collections;
using UnityEngine;

namespace Nebby
{
    public class SpriteLocator : MonoBehaviour
    {
        [Tooltip("The Transform of the child game object which acts as the sprite for this entity")]
        public Transform _spriteTransform;
        [Tooltip("The transform of the child gameObject which acts as the base for this entity's sprite. If provided, this will be detached from the hierarchy and positioned to match this object's position")]
        public Transform spriteBaseTransform;
        public bool dontDetatchFromParent;
        private void Start()
        {
            if(_spriteTransform)
            {
                if(!dontDetatchFromParent)
                {
                    spriteBaseTransform.parent = null;
                }
            }
        }
    }
}