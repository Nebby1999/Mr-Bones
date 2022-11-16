using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Nebby;

namespace Nebby
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundDefPool : MonoBehaviour
    {
        [SerializeField] private string customName;
        [SerializeField] private SoundDef[] soundDefs;
        [SerializeField] private bool fadeOutOnStop;
        [SerializeField] private AudioSource audioSource;

        public AudioSource AudioSource => audioSource;
        private IEnumerator fadeOutCoroutine;

        private SoundDef currentSoundDef;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public virtual void PlayRandomSound()
        {
            if (fadeOutCoroutine != null)
                StopCoroutine(fadeOutCoroutine);
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

        public virtual void StopCurrentSound()
        {
            if(audioSource.clip != currentSoundDef.clip)
            {
                Debug.LogError($"The AudioSource component's clip is not {currentSoundDef}'s clip! {currentSoundDef.clip}");
            }

            if(!audioSource.isPlaying)
            {
                Debug.LogWarning($"Cannot stop sound when its not playing!");
                return;
            }

            if (fadeOutOnStop)
            {
                fadeOutCoroutine = BeginFadeOut(0.25f);
                StartCoroutine(fadeOutCoroutine);
            }
            else
                AudioSource.Stop();
        }
        protected IEnumerator BeginFadeOut(float fadeTime)
        {
            float startVolume = AudioSource.volume;
            while(AudioSource.volume > 0)
            {
                AudioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
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

        public static SoundDefPool FindByCustomName(GameObject obj, string customName)
        {
            SoundDefPool[] pools = obj.GetComponents<SoundDefPool>();
            for(int i = 0; i < pools.Length; i++)
            {
                if (string.CompareOrdinal(pools[i].customName, customName) == 0)
                {
                    return pools[i];
                }
            }
            return null;
        }
    }
}
