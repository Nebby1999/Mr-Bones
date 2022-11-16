using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrBones
{
	[RequireComponent(typeof(CharacterSpirit), typeof(PlayerInput))]
	public class MrBonesSpirit : MonoBehaviour
	{
		private Vector2 movementInput;

		private Vector2 rawAimVector;

		private Vector2 aimVector;

		private InputActionPhase breakingInput;

		private InputActionPhase hoppingInput;

		private CharacterBody body;

		private MrBonesMovement bodyMovement;

		private InputSimulator bodyInputs;

		private Camera mainCam;

		public PlayerInput PlayerInput { get; private set; }

		public CharacterSpirit Spirit { get; private set; }

		public bool IsUsingMouseInput => PlayerInput.FindFirstPairedToDevice(Mouse.current) == PlayerInput;

		public static event Action<MrBonesSpirit> OnMrBonesSpawned;

		private void Awake()
		{
			PlayerInput = GetComponent<PlayerInput>();
			Spirit = GetComponent<CharacterSpirit>();
			Spirit.OnBodySpawned += OnBodySpawned;
			mainCam = Camera.main;
		}

		private void OnBodySpawned(CharacterBody obj)
		{
			OnMrBonesSpawned?.Invoke(this);
		}

		private void FixedUpdate()
		{
			SetBody(Spirit.BodyObjectInstance);
			if (body)
			{
				bodyInputs.movementInput = movementInput;
				Vector2 normalized = rawAimVector;
				if (IsUsingMouseInput)
				{
					Vector2 val = mainCam.ScreenToWorldPoint(rawAimVector);
					Vector2 val2 = Spirit.BodyObjectInstance ? Spirit.BodyObjectInstance.transform.position : transform.position;
					Vector2 val3 = val - val2;
					normalized = val3.normalized;
				}
				aimVector = normalized;
				bodyInputs.AimDirection = aimVector;
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			movementInput = context.ReadValue<Vector2>();
		}

		public void OnAim(InputAction.CallbackContext context)
		{
			rawAimVector = context.ReadValue<Vector2>();
		}

		public void OnHop(InputAction.CallbackContext context)
		{
			InputActionPhase phase = context.phase;
			bodyInputs.jump.SetPhase(phase);
		}

		public void OnBreak(InputAction.CallbackContext context)
		{
			InputActionPhase phase = context.phase;
			bodyInputs.breaking.SetPhase(phase);
		}

		public void OnFire(InputAction.CallbackContext context)
		{
			float fireInput = context.ReadValue<float>();
			InputActionPhase phase = context.phase;
			bodyInputs.fireInput = fireInput;
			bodyInputs.fire.SetPhase(phase);
		}

		public void OnFire2(InputAction.CallbackContext context)
		{
			InputActionPhase phase = context.phase;
			bodyInputs.fire2.SetPhase(phase);
		}

		private void SetBody(GameObject newBody)
		{
			body = newBody ? newBody.GetComponent<CharacterBody>() : null;
			if (body)
			{
				bodyMovement = body.GetComponent<MrBonesMovement>();
				bodyInputs = body.InputSimulator;
			}
		}
	}
}