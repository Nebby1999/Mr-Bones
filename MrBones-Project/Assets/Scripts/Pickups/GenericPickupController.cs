using System;
using System.Collections;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;
using UnityEngine;
using UObject = UnityEngine.Object;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MrBones.Pickups
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class GenericPickupController : MonoBehaviour
    {
        public PickupDef pickupDef;
        [SerializeField] private TagObject triggerColliderTag;
        [SerializeField] private bool hasGravity = true;
        [SerializeField] private bool scaleToPickupDefScale = true;
        [SerializeField] private bool destroyOnCollected = true;
        [SerializeField] private PolygonCollider2D polygonCollider;
        public UnityEvent OnPickupCollected;
        public PolygonCollider2D PolygonCollider2D => polygonCollider;
        public CircleCollider2D TriggerCollider { get; private set; }
        public Rigidbody2D RigidBody2D { get; private set; }

        private void OnValidate()
        {
            if (polygonCollider)
                polygonCollider.isTrigger = false;
            GetComponent<CircleCollider2D>().isTrigger = true;

            if(pickupDef)
            {
                SetSpriteAndCollider();
            }
        }

        private void Awake()
        {
            TriggerCollider = GetComponent<CircleCollider2D>();
            RigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            transform.localScale = scaleToPickupDefScale ? Vector3.one * pickupDef.scale : transform.localScale; 

            if (!hasGravity)
                RigidBody2D.gravityScale = 0f;

            SetSpriteAndCollider();
        }

        private void SetSpriteAndCollider()
        {
            SpriteRenderer spriteRenderer = PolygonCollider2D.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = pickupDef.pickupSprite;
            
            UpdateShapeToSprite(PolygonCollider2D, pickupDef.pickupSprite);
        }

        private void UpdateShapeToSprite(PolygonCollider2D collider, Sprite sprite)
        {
            // ensure both valid
            if (collider != null && sprite != null)
            {
                // update count
                collider.pathCount = sprite.GetPhysicsShapeCount();

                // new paths variable
                List<Vector2> path = new List<Vector2>();

                // loop path count
                for (int i = 0; i < collider.pathCount; i++)
                {
                    // clear
                    path.Clear();
                    // get shape
                    sprite.GetPhysicsShape(i, path);
                    // set path
                    collider.SetPath(i, path.ToArray());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject root = other.gameObject.GetRootGameObject();
            TagContainer container = root.GetComponentInParent<TagContainer>();
            if(!container)
                return;

            if (!container.ObjectHasTag(triggerColliderTag))
                return;

            IPickupCollector pickupCollector = root.GetComponent<IPickupCollector>();
            if (pickupCollector != null)
            {
                bool success = pickupCollector.OnPickupInteraction(gameObject, pickupDef);
                if(success)
                {
                    OnPickupCollected?.Invoke();
                    if(destroyOnCollected)
                        Destroy(gameObject);
                }
            }
        }
    }
}