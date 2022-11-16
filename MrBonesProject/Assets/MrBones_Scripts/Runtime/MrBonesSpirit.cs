using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrBones
{
	[RequireComponent(typeof(CharacterSpirit), typeof(PlayerInput))]
	public class MrBonesSpirit : MonoBehaviour
	{
		public PlayerInput PlayerInput { get; private set; }
		public CharacterSpirit Spirit { get; private set; }
		public bool IsUsingMouseInput => PlayerInput.FindFirstPairedToDevice(Mouse.current) == PlayerInput;
		public bool centerScreenMouseInput = true;
		public static event Action<MrBonesSpirit> OnMrBonesSpawned;

		private CharacterBody body;
		private Camera mainCam;
		private InputSimulator bodyInputs;
		private Vector2 movementInput;
		private Vector2 rawAimVector;
		private Vector2 aimVector;
		private Vector2Int screenRes;
		private Vector2Int centerScreen;

		private void Awake()
		{
			PlayerInput = GetComponent<PlayerInput>();
			Spirit = GetComponent<CharacterSpirit>();
			Spirit.OnBodySpawned += OnBodySpawned;
			mainCam = Camera.main;
			screenRes = new Vector2Int(Screen.width, Screen.height);
			centerScreen = screenRes / 2;
		}

		private void OnBodySpawned(CharacterBody obj)
		{
			OnMrBonesSpawned?.Invoke(this);
		}

        private void Update()
        {
            if(screenRes.x != Screen.width || screenRes.y != Screen.height)
            {
				screenRes = new Vector2Int(Screen.width, Screen.height);
				centerScreen = screenRes / 2;
            }
        }
        private void FixedUpdate()
		{
			SetBody(Spirit.BodyObjectInstance);
			if (body)
			{
				bodyInputs.movementInput = movementInput;
				aimVector = IsUsingMouseInput ? GetMouseInput() : rawAimVector;
				bodyInputs.AimDirection = aimVector;
			}
		}

		public Vector2 GetMouseInput()
        {
			Vector2 mousePoint = mainCam.ScreenToWorldPoint(rawAimVector);
			Vector2 subtrahend = centerScreenMouseInput ? mainCam.ScreenToWorldPoint((Vector2)centerScreen) : (body ? body.transform.position : transform.position);
			Vector2 val3 = mousePoint - subtrahend;
			return val3.normalized;
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
				bodyInputs = body.InputSimulator;
			}
		}
	}
}