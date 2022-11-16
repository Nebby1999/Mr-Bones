using Nebby;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrBones
{
    [RequireComponent(typeof(InputSimulator), typeof(Rigidbody2D), typeof(CharacterBody))]
    public class MrBonesMovement : MonoBehaviour
    {
        public InputSimulator InputSimulator { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public CharacterBody CharacterBody { get; private set; }
        public Collider2D Collider { get; private set; }
        public bool Breaking => breaking;
        public bool Grounded => grounded;

        public Vector2 maxVelocity;
        public float breakCoefficient;

        [Header("Hop Settings")]
        public float minimumHopStrength;
        public float maximumHopStrength;
        public float hopStrengthDivisor;

        [Header("Ground Check Settings")]
        public LayerMask groundLayers;
        public float extraBoxCastHeight;
        [Range(0, 1)] public float maxCoyoteTime;

        [Space(5), Header("Read Only")]
        [ReadOnly, SerializeField] private Vector2 currentVelocity;
        [ReadOnly, SerializeField] private bool breaking;
        [ReadOnly, SerializeField] private bool grounded;
        [ReadOnly, SerializeField] private bool canJump;
        [ReadOnly, SerializeField] private int timesJumped;

        private bool hasJumped;
        private float coyoteTimer;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            CharacterBody = GetComponent<CharacterBody>();
            InputSimulator = GetComponent<InputSimulator>();
            Collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            InputSimulator.jump.OnStarted += OnHop;
        }

        private void OnDisable()
        {
            InputSimulator.jump.OnStarted -= OnHop;
        }

        private void FixedUpdate()
        {
            grounded = IsGrounded();
            if(grounded && hasJumped)
            {
                hasJumped = false;
            }
            canJump = CanJump();
            SetVelocity();
            MovementByInput();
            if(InputSimulator.jump.Started)
            {
                OnHop();
            }
        }

        public void DoJetpackBoost(Vector2 direction, float force)
        {
            Rigidbody2D.AddForce(direction * force, ForceMode2D.Force);
        }

        public void Burst(Vector2 direction, float strength)
        {
            Rigidbody2D.AddForce(-direction * strength, ForceMode2D.Impulse);
        }
        private bool IsGrounded()
        {
            RaycastHit2D raycast = Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0, Vector2.down, extraBoxCastHeight, groundLayers);
#if UNITY_EDITOR
            Color rayColor = raycast ? Color.green : Color.red;
            Debug.DrawRay(Collider.bounds.center + new Vector3(Collider.bounds.extents.x, 0), Vector2.down * (Collider.bounds.extents.y + extraBoxCastHeight), rayColor);
            Debug.DrawRay(Collider.bounds.center - new Vector3(Collider.bounds.extents.x, 0), Vector2.down * (Collider.bounds.extents.y + extraBoxCastHeight), rayColor);
            Debug.DrawRay(Collider.bounds.center - new Vector3(Collider.bounds.extents.x, Collider.bounds.extents.y + extraBoxCastHeight), Vector2.right * (Collider.bounds.extents.x * 2f), rayColor);
#endif
            if(!raycast)
            {
                return IsCoyoteTime();
            }
            coyoteTimer = 0;

            if(!hasJumped)
                timesJumped = 0;

            return raycast;
        }

        private bool IsCoyoteTime()
        {
            coyoteTimer += 1 * Time.fixedDeltaTime;
            return coyoteTimer < maxCoyoteTime;
        }

        private bool CanJump()
        {
            if(timesJumped < CharacterBody.JumpCount)
            {
                if(grounded)
                {
                    return timesJumped <= CharacterBody.JumpCount - 1;
                }
                return true;
            }
            return false;
        }

        private void SetVelocity()
        {
            float yVelocity = CalculateVelocity(maxVelocity.y, Rigidbody2D.velocity.y);
            float xVelocity = CalculateVelocity(maxVelocity.x, Rigidbody2D.velocity.x);

            if(InputSimulator.breaking.Performed && grounded)
            {
                xVelocity *= breakCoefficient;
                var absVelocity = Mathf.Abs(xVelocity);
                if(absVelocity <= 0.1)
                {
                    xVelocity = 0;
                }
            }

            currentVelocity = new Vector2(xVelocity, yVelocity);
            Rigidbody2D.velocity = currentVelocity;

            float CalculateVelocity(float max, float current)
            {
                bool shouldReturnNegative = (current < 0 && max > 0);
                float newVelocity = Mathf.Min(Mathf.Abs(max), Mathf.Abs(current));
                return shouldReturnNegative ? -newVelocity : newVelocity;
            }
        }

        private void MovementByInput()
        {
            var force = CharacterBody.Speed * Time.fixedDeltaTime * InputSimulator.movementInput;
            Rigidbody2D.AddForce(new Vector2(force.x, 0), ForceMode2D.Force);
        }

        private void OnHop()
        {
            if(canJump)
            {
                timesJumped++;
                hasJumped = true;
                var xVelocity = Rigidbody2D.velocity.x;
                var hopStr = Mathf.Abs(xVelocity/hopStrengthDivisor);
                hopStr = Mathf.Max(hopStr, minimumHopStrength);
                hopStr = Mathf.Min(hopStr, maximumHopStrength);
                Rigidbody2D.AddForce(new Vector2(0, hopStr), ForceMode2D.Impulse);
            }
        }
    }
}
