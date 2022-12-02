using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Nebby
{
    public class OnPauseEvent : MonoBehaviour
    {
        public UnityEvent OnPause;
        public UnityEvent OnUnpause;
        private void OnEnable()
        {
            PauseManager.OnPauseChange += InvokeEvents;
        }

        private void InvokeEvents(bool shouldPause)
        {
            if (shouldPause)
                OnPause?.Invoke();
            else
                OnUnpause?.Invoke();
        }

        private void OnDisable()
        {
            PauseManager.OnPauseChange -= InvokeEvents;
        }
    }
}