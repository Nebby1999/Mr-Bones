using Nebby;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrBones
{
    public class OOB : SceneRestarter
    {
        public SoundDef deathSFX;
        private MrBonesSpirit spirit;

        protected override void Awake()
        {
            base.Awake();
            MrBonesSpirit.OnMrBonesSpawned += MrBonesSpirit_OnMrBonesSpawned;
        }

        private void OnDestroy()
        {
            MrBonesSpirit.OnMrBonesSpawned -= MrBonesSpirit_OnMrBonesSpawned;
        }

        private void MrBonesSpirit_OnMrBonesSpawned(MrBonesSpirit obj)
        {
            spirit = obj;
        }

        protected override void RestartScene()
        {
            spirit.SetIgnoringInput(true);
            StartCoroutine(C_SceneRestarting());
        }

        private IEnumerator C_SceneRestarting()
        {
            deathSFX.Play();

            yield return new WaitForSeconds(deathSFX.clip.length);
            base.RestartScene();
        }
    }
}