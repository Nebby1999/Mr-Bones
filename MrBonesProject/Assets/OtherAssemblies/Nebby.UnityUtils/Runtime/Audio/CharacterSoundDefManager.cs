using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nebby
{
    [DisallowMultipleComponent]
    public class CharacterSoundDefManager : MonoBehaviour
    {
        public bool pauseSoundsOnGamePause = true;
        [ReadOnly] public GameObject audioSourceHolder;

        public readonly Dictionary<SoundDef, AudioSource> soundDefToTiedSource = new();

        private void Awake()
        {
            if(!audioSourceHolder)
            {
                audioSourceHolder = new GameObject($"{gameObject.name}_SoundDefManager");
                audioSourceHolder.hideFlags = HideFlags.HideInHierarchy;
            }
        }

        private void OnEnable()
        {
            PauseManager.OnPauseChange -= PauseOrUnpauseAllSources;
            if(pauseSoundsOnGamePause)
                PauseManager.OnPauseChange += PauseOrUnpauseAllSources;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseChange -= PauseOrUnpauseAllSources;
        }

        private void PauseOrUnpauseAllSources(bool shouldPause)
        {
            foreach(AudioSource source in soundDefToTiedSource.Values)
            {
                if (shouldPause)
                    source.Pause();
                else
                    source.UnPause();
            }
        }

        public void PlaySound(SoundDef sound)
        {
            //If an audioSource already manages the soundDef, just reuse it.
            if(soundDefToTiedSource.TryGetValue(sound, out AudioSource audioSource))
            {
                if (audioSource.isPlaying)
                    return;

                audioSource.SetSettingsToSoundDef(sound);
                audioSource.Play();
                return;
            }
            var newAudioSource = audioSourceHolder.AddComponent<AudioSource>();
            newAudioSource.SetSettingsToSoundDef(sound);
            soundDefToTiedSource[sound] = newAudioSource;
            newAudioSource.Play();
        }

        public bool TryStopSound(SoundDef sound)
        {
            if(!soundDefToTiedSource.ContainsKey(sound))
            {
                Debug.LogWarning($"Cannot stop sound {sound} as {this} does not have an audioSource that plays the given soundDef");
                return false;
            }

            var audioSource = soundDefToTiedSource[sound];
            if(!audioSource.isPlaying)
            {
                Debug.LogWarning($"Cannot stop sound {sound} as the tied audio source is not playing the sound.");
                return false;
            }

            audioSource.Stop();
            return true;
        }
    }
}
