using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrBones
{
    public class InputSimulator : MonoBehaviour
    {
        public class SimulatedButton
        {
            private InputActionPhase currentPhase;
            public bool Canceled => currentPhase == InputActionPhase.Canceled;
            public bool Started => currentPhase == InputActionPhase.Started;
            public bool Waiting => currentPhase == InputActionPhase.Waiting;
            public bool Performed => currentPhase == InputActionPhase.Performed;
            public event Action OnStarted;
            public event Action OnCanceled;
            public void SetPhase(InputActionPhase newPhase)
            {
                if(newPhase == InputActionPhase.Started && Canceled)
                {
                    OnStarted?.Invoke();
                }
                if(newPhase == InputActionPhase.Canceled && Performed)
                {
                    OnCanceled?.Invoke();
                }
                currentPhase = newPhase;
            }
        }
        public SimulatedButton jump = new SimulatedButton();
        public SimulatedButton breaking = new SimulatedButton();
        public SimulatedButton fire = new SimulatedButton();
        public SimulatedButton fire2 = new SimulatedButton();
        public Vector2 movementInput;
        public Vector2 AimDirection
        {
            get
            {
                if (_aimDirection == Vector2.zero)
                {
                    return transform.up;
                }
                return _aimDirection;
            }
            set
            {
                _aimDirection = value.normalized;
            }
        }
        public Vector2 AimInput => _aimDirection;
        private Vector2 _aimDirection;
        public float fireInput;
    }
}