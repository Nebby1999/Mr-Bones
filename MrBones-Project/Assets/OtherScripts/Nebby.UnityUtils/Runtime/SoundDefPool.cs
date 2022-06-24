using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Nebby.CSharpUtils;

namespace Nebby.UnityUtils
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundDefPool : MonoBehaviour
    {
        [SerializeField] private SoundDef[] soundDefs;

        private SoundDef currentSoundDef;
        public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        public void PlayRandomSound()
        {
            if(soundDefs.TryGetRandomElement(out SoundDef soundDef))
            {
                currentSoundDef = soundDef;
            }
            else
            {
                Debug.LogError($"Failed to get random soundDef from array");
                return;
            }

            SetAudioSourceSettings();
            AudioSource.Play();
        }

        public void StopCurrentSound()
        {
            if(AudioSource.clip != currentSoundDef.clip)
            {
                Debug.LogError($"The AudiSource component's clip is not {currentSoundDef}'s clip! {currentSoundDef.clip}");
            }

            if(!AudioSource.isPlaying)
            {
                Debug.LogWarning($"Cannot stop sound when its not playing!");
                return;
            }

            AudioSource.Stop();
        }
        private void SetAudioSourceSettings()
        {
            AudioSource.volume = currentSoundDef.Volume;
            AudioSource.pitch = currentSoundDef.Pitch;
            AudioSource.clip = currentSoundDef.clip;
            AudioSource.loop = currentSoundDef.looping;
        }
    }
}
