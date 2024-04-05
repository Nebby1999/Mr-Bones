using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby;

namespace MrBones
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D), typeof(IPickable))]
    public class GenericPickupController : MonoBehaviour
    {
        public IPickable PickableBehaviour { get; private set; }
        [Tooltip("Pickups not affected by gravity bob in the air")]
        public bool affectedByGravity;

        public float bobDelay;
        [Tooltip("If null, the transform to bob will be the transform attached to this behaviour")]
        public Transform transformToBob;
        public Vector3 bobDistance = Vector3.zero;
        public Color pickupColor;
        public SoundDef pickUpSound;
        private Vector3 initialPos;

        private void Awake()
        {
            PickableBehaviour = GetComponentInChildren<IPickable>();
            if (PickableBehaviour == null)
                Debug.LogWarning($"{gameObject} has a GenericPickupController, but the controller couldnt find a behaviour that implements IPickable!");

            if (!transformToBob)
                transformToBob = transform;

            if (affectedByGravity)
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }

        private void Start()
        {
            if (transformToBob.parent)
            {
                initialPos = transformToBob.localPosition;
            }
            else
            {
                initialPos = transformToBob.position;
            }
        }

        private void Update()
        {
            if (!affectedByGravity)
                Bob();
        }

        private void Bob()
        {
            Vector3 vector = initialPos + bobDistance * Mathf.Sin(Time.fixedTime - bobDelay);
            if (transformToBob.parent)
            {
                transformToBob.localPosition = vector;
            }
            else
            {
                transformToBob.position = vector;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var pickupInfo = GeneratePickupInfo(collision.GetRootGameObject());
            if (PickableBehaviour != null && PickableBehaviour.ShouldGrantPickup(pickupInfo))
            {
                PickableBehaviour.GrantPickupToPicker(pickupInfo);
                GameEventManager.Instance?.OnPickup(pickupInfo);
                Destroy(gameObject);
            }
        }

        private PickupInfo GeneratePickupInfo(GameObject pickerRoot)
        {
            var pickupInfo = new PickupInfo();
            pickupInfo.pickableObject = PickableBehaviour;
            pickupInfo.controller = this;
            pickupInfo.pickerObject = pickerRoot;
            var body = pickerRoot.GetComponent<CharacterBody>();
            pickupInfo.pickerBody = body;
            if (body)
                pickupInfo.pickerSpirit = body.Spirit;

            return pickupInfo;
        }
    }
}
