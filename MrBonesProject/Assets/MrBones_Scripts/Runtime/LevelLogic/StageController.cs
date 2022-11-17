using System.Collections;
using UnityEngine;
using Nebby;
using UnityEngine.SceneManagement;

namespace MrBones
{
    [RequireComponent(typeof(TimerComponent))]
    public class StageController : SingletonBehaviour<StageController>
    {
        public StageDef tiedStage;
        public TimerComponent Timer { get; private set; }

        private void OnValidate()
        {
            if(!tiedStage)
            {
                Debug.LogError($"No stage provided for {this}!", this);
            }
        }
        private void Awake()
        {
            Timer = GetComponent<TimerComponent>();
        }

        public void Restart()
        {
            tiedStage.sceneToLoad.LoadScene();
        }

        public void OnMilkCollected()
        {
            StartCoroutine(StageCompleted());
        }

        private IEnumerator StageCompleted()
        {
            if (tiedStage.nextStage)
            {
                var op = tiedStage.nextStage.sceneToLoad.LoadSceneAsync();
                while (!op.isDone)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                SceneManager.LoadScene("mainmenu");
            }
        }
    }
}