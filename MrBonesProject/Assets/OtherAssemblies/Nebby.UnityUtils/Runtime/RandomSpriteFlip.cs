using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nebby
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomSpriteFlip : MonoBehaviour
    {
        [SerializeField] bool _flipX;
        [SerializeField] bool _flipY;
        [SerializeField] bool _destroyAfterFlip;

        public SpriteRenderer Renderer { get; private set; }
        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
            Flip();
        }

        public void Flip()
        {
            if (_flipX)
                Renderer.flipX = Random.Range(0, 1) == 0 ? false : true;
            if (_flipY)
                Renderer.flipY = Random.Range(0, 1) == 0 ? false : true;

            if (_destroyAfterFlip)
                Destroy(this);
        }
    }
}
