using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public static class PauseManager
    {
        /// <summary>
        /// Delegate for handling a callback when the game is paused
        /// </summary>
        /// <param name="shouldPause">Wether the game is going to be paused or not, True if it'll be paused, False if it'll be unpaused</param>
        public delegate void PauseCallback(bool shouldPause);
        public static bool IsPaused => Time.timeScale == 0;
        public static event PauseCallback OnPauseChange;
        /// <summary>
        /// Called when the game is Paused
        /// </summary>
        public static event Action OnPause;
        /// <summary>
        /// Called when the game is Unpaused
        /// </summary>
        public static event Action OnUnpause;

        /// <summary>
        /// Sets wether the game gets paused or unpaused, and invokes callbacks.
        /// </summary>
        /// <param name="shouldPause">Wether to pause or unpause the game</param>
        public static void SetPause(bool shouldPause)
        {
            Time.timeScale = shouldPause ? 0 : 1;
            OnPauseChange?.Invoke(shouldPause);

            if (shouldPause)
                OnPause?.Invoke();
            else
                OnUnpause?.Invoke();
        }

        public static void Switch()
        {
            SetPause(!IsPaused);
        }

        public static void PauseGame()
        {
            SetPause(true);
        }

        public static void UnpauseGame()
        {
            SetPause(false);
        }
    }
}