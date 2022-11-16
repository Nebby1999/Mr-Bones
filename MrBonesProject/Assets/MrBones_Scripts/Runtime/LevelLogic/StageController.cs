using System.Collections;
using UnityEngine;
using Nebby;
using UnityEngine.SceneManagement;

namespace MrBones.Stages
{
    [RequireComponent(typeof(TimerComponent))]
    public class StageController : MonoBehaviour
    {
        public StageDef tiedStage;
        public TimerComponent Timer { get; private set; }

        private void Awake()
        {
            Timer = GetComponent<TimerComponent>();
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