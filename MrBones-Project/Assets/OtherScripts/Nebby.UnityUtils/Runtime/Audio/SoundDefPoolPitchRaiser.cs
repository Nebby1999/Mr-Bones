using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby.UnityUtils
{
    public class SoundDefPoolPitchRaiser : SoundDefPool
    {
        public float raiseTime;

        private IEnumerator pitchRaiseCoroutine;
        public override void PlayRandomSound()
        {
            if (pitchRaiseCoroutine != null)
                StopCoroutine(pitchRaiseCoroutine);
            base.PlayRandomSound();
            pitchRaiseCoroutine = BeginPitchRaiser(raiseTime);
            StartCoroutine(pitchRaiseCoroutine);
        }

        public override void StopCurrentSound()
        {
            base.StopCurrentSound();
            if(pitchRaiseCoroutine != null)
                StopCoroutine(pitchRaiseCoroutine);
        }

        private IEnumerator BeginPitchRaiser(float raiseTime)
        {
            float startPitch = AudioSource.pitch;
            float maxPitch = AudioSource.pitch * 5;
            while(AudioSource.pitch <= maxPitch)
            {
                AudioSource.pitch += startPitch * Time.deltaTime / raiseTime;
                yield return null;
            }
        }
    }
}