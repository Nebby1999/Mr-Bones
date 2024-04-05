using Nebby;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrBones.UI
{
    public class UIStageTransition : MonoBehaviour
    {
        private SceneReference sceneToLoadAsynchronously;

        public SceneReference mainMenuScene;
        public UIScaleWithCurve scaler;
        public UIScaleWithCurve unscaler;

        public void DoTransition(SceneReference sceneToLoad)
        {
            gameObject.SetActive(true);
            sceneToLoadAsynchronously = sceneToLoad;
            StartCoroutine(C_StageTransition());
        }

        private IEnumerator C_StageTransition()
        {
            PauseManager.canPause = false;
            SceneRestarter.canRestart = false;

            scaler.BeginScaling();

            while (scaler.Scaling)
                yield return null;

            AsyncOperation operation = string.IsNullOrEmpty(sceneToLoadAsynchronously.SceneName) ? mainMenuScene.LoadSceneAsync() : sceneToLoadAsynchronously.LoadSceneAsync();
            while (!operation.isDone && !StageController.Instance)
                yield return new WaitForEndOfFrame();

            unscaler.BeginScaling();
            while (unscaler.Scaling)
                yield return null;

            Destroy(gameObject);
            PauseManager.canPause = true;
            SceneRestarter.canRestart = true;
            yield break;
        }
    }
}