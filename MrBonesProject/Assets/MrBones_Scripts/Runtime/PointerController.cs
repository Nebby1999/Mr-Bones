using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PointerController : MonoBehaviour
    {
        [Tooltip("The InputSimulator from where we'll get the LookDirection")]
        public InputSimulator inputSimulator;
        public SpriteRenderer SpriteRenderer { get; set; }
        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            SpriteRenderer.enabled = inputSimulator.AimInput != Vector2.zero;
            transform.localPosition = new Vector3(inputSimulator.AimInput.x, inputSimulator.AimInput.y, transform.localPosition.z);

            Quaternion lookRot = Quaternion.LookRotation(Vector3.forward, inputSimulator.AimInput);
            transform.rotation = lookRot;
        }
    }
}
