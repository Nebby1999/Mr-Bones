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
        public bool hasShownMilkLocation;
        public MilkPickup stageMilk;
        public TimerComponent Timer { get; private set; }
        public override bool DestroyIfDuplicate => true;

        public void Restart()
        {
            tiedStage.sceneToLoad.LoadScene();
        }

        public void OnMilkCollected()
        {
            GameObject newGO = new GameObject();
            newGO.name = "StageController Destroyer";
            transform.parent = newGO.transform;
            StartCoroutine(StageCompleted());
        }

        private void OnValidate()
        {
            if(!tiedStage)
            {
                Debug.LogError($"No stage provided for {this}!", this);
            }
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Timer = GetComponent<TimerComponent>();

            if(!Instance)
                MrBonesSpirit.OnMrBonesSpawned += ShowMilkLoaction;
        }

        private void ShowMilkLoaction(MrBonesSpirit obj)
        {
            if(!hasShownMilkLocation)
            {
                hasShownMilkLocation = true;
                StartCoroutine(ShowMilk(obj));
                return;
            }
            obj.SetIgnoringInput(false);
        }

        private void OnDestroy()
        {
            MrBonesSpirit.OnMrBonesSpawned -= ShowMilkLoaction;
        }

        private IEnumerator ShowMilk(MrBonesSpirit spirit)
        {
            Timer.timeElapsed = 0;
            Timer.enabled = false;
            spirit.SetIgnoringInput(true);
            
            yield return new WaitForEndOfFrame();

            CinemachineMainCamera.Instance.PanToObject(stageMilk.gameObject);

            yield return new WaitForSeconds(2);

            CinemachineMainCamera.Instance.PanToObject(null);

            spirit.SetIgnoringInput(false);
            Timer.enabled = true;
            yield break;
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