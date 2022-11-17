using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrBones
{
    public class StageLoader : MonoBehaviour
    {
        public StageDef stageToLoad;


        public void LoadStage()
        {
            stageToLoad.sceneToLoad.LoadScene();
        }

        public void LoadStageAsync()
        {
            StartCoroutine(LoadStageAsyncInternal());
        }

        private IEnumerator LoadStageAsyncInternal()
        {
            var request = stageToLoad.sceneToLoad.LoadSceneAsync();
            while(!request.isDone)
            {
                Debug.Log($"Loading Scene {stageToLoad.sceneToLoad.SceneName} ({request.progress * 100}%)");
                yield return new WaitForEndOfFrame();
            }
        }
    }
}