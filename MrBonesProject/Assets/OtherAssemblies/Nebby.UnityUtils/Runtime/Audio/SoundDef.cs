using UnityEngine.Audio;
using UnityEngine;

namespace Nebby
{
    [CreateAssetMenu(menuName = "Nebby/SoundDef", fileName = "New SoundDef")]
    public class SoundDef : ScriptableObject
    {
        public AudioClip clip;
        public bool looping;
        [SerializeField] private FloatMinMax volume = new FloatMinMax(1, 10);
        [SerializeField] private FloatMinMax pitch = new FloatMinMax(-5, 5);
        [SerializeField] private float constantVolume;
        [SerializeField] private float constantPitch;

        private static GameObject audioSourceManagerObject;

        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public bool volumeIsConstant = false;
        [HideInInspector] public bool pitchIsConstant = false;

        public float Volume => volumeIsConstant ? constantVolume : volume.GetRandomRange();
        public float Pitch => pitchIsConstant ? constantPitch : pitch.GetRandomRange();

        public void Play()
        {
            if (audioSourceManagerObject == null)
            {
                if (audioSource)
                    audioSourceManagerObject = audioSource.gameObject;
                else
                    audioSourceManagerObject = new GameObject();
            }

            if (audioSource == null)
                audioSource = audioSourceManagerObject.AddComponent<AudioSource>();

            if (audioSource.isPlaying)
                return;

            audioSource.SetSettingsToSoundDef(this);
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}