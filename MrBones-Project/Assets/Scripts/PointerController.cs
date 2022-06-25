using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PointerController : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Vector2 LookDirection { get; set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            SpriteRenderer.enabled = LookDirection != Vector2.zero;
            transform.localPosition = new Vector3(LookDirection.x, LookDirection.y, transform.localPosition.z);

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, LookDirection);
            transform.rotation = lookRotation;
        }
    }
}
