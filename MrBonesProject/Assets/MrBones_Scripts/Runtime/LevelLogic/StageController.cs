using System.Collections;
using UnityEngine;
using Nebby;
using UnityEngine.SceneManagement;
using MrBones.UI;

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

        private GameObject stageTransitionPrefab;

        public void Restart()
        {
            tiedStage.sceneToLoad.LoadScene();
        }

        public void OnMilkCollected()
        {
            GameObject newGO = new GameObject();
            newGO.name = "StageController Destroyer";
            transform.parent = newGO.transform;

            UIStageTransition stageTransition = Instantiate(stageTransitionPrefab).GetComponent<UIStageTransition>();
            DontDestroyOnLoad(stageTransition.gameObject);
            stageTransition.DoTransition(tiedStage.nextStage ? tiedStage.nextStage.sceneToLoad : SceneReference.Null);
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
            stageTransitionPrefab = Resources.Load<GameObject>("LoadingTransition");
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

            yield return new WaitForSeconds(5);

            CinemachineMainCamera.Instance.PanToObject(null);

            spirit.SetIgnoringInput(false);
            Timer.enabled = true;
            yield break;
        }
    }
}