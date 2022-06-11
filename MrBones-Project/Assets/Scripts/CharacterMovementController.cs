using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Nebby.CSharpUtils;

namespace MrBones
{
    public class CharacterMovementController : MonoBehaviour
    {
        [Range(0, 0.3f)]
        public float movementSmoothing = 0.05f;
        public bool hasAirControl = false;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private Transform _groundCheck;

        private const float _groundedRadius = 0.2f;
        private const float _ceilingRadius = 0.2f;
        private bool _grounded;
        private Vector2 _velocity = Vector2.zero;
        public Rigidbody2D RigidBody2D { get; private set; }

        private void Awake()
        {
            RigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            bool wasGrounded = _grounded;
            _grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _groundLayers);
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    _grounded = true;
                }
            }
        }

        public void NewMove(Vector2 direction)
        {
            if(_grounded || hasAirControl)
            {
                RigidBody2D.AddForce(new Vector2(direction.x, 0));
            }
        }

        public void Recoil(Vector2 recoilDirection, float recoilStrength)
        {
            var flippedVector2 = -recoilDirection;
            RigidBody2D.AddForce(flippedVector2 * recoilStrength, ForceMode2D.Impulse);
        }
    }
}
