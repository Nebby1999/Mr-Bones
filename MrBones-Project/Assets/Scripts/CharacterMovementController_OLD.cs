using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Nebby.CSharpUtils;

namespace MrBones
{
    public class CharacterMovementController_OLD : MonoBehaviour
    {
        [Range(0, 0.3f)]
        public float movementSmoothing = 0.05f;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Vector2 _maxVelocityOnGround;
        [SerializeField] private Vector2 _maxVelocityOnAir;
        private const float _groundedRadius = 0.2f;
        private const float _ceilingRadius = 0.2f;
        public bool _grounded;
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

            SetVelocity();
        }

        private void SetVelocity()
        {
            var chosenMaxVelocity = _grounded ? _maxVelocityOnGround : _maxVelocityOnAir;
            float yVelocity = CalculateVelocity(chosenMaxVelocity.y, RigidBody2D.velocity.y);
            float xVelocity = CalculateVelocity(chosenMaxVelocity.x, RigidBody2D.velocity.x);

            RigidBody2D.velocity = new Vector2(xVelocity, yVelocity);
        }

        private float CalculateVelocity(float max, float current)
        {
            bool shouldReturnNegative = false;
            
            //Both current and max are negative? return a negative value
            if (current < 0 && max > 0)
                shouldReturnNegative = true;

            float newVelocity = Mathf.Min(Mathf.Abs(max), Mathf.Abs(current));
            return shouldReturnNegative ? -newVelocity : newVelocity;
        }
        public void NewMove(Vector2 direction)
        {
            RigidBody2D.AddForce(new Vector2(direction.x, 0));
            /*if(_grounded || hasAirControl)
            {
                RigidBody2D.AddForce(new Vector2(direction.x, 0));
            }*/
        }

        public void Recoil(Vector2 recoilDirection, float recoilStrength)
        {
            var flippedVector2 = -recoilDirection;
            RigidBody2D.AddForce(flippedVector2 * recoilStrength, ForceMode2D.Impulse);
        }
    }
}
