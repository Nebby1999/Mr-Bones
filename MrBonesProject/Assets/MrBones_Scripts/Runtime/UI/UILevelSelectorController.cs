using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MrBones;
using UnityEngine.UI;

namespace MrBones.UI
{
    public class UILevelSelectorController : MonoBehaviour
    {
        [SerializeField] private GameObject levelSelectButtonPrefab;
        [SerializeField] private UILevelInfoShower levelInfoShower;
        [SerializeField] private GridLayoutGroup gridLayoutForButtons;
        [SerializeField] private StageDef[] allStages;

        private void Start()
        {
            Transform gridLayoutTransform = gridLayoutForButtons.transform;
            for(int i = 0; i < allStages.Length; i++)
            {
                StageDef stage = allStages[i];
                var buttonInstance = Instantiate(levelSelectButtonPrefab, gridLayoutTransform);
                var uiLevelButton = buttonInstance.GetComponent<UILevelButton>();
                uiLevelButton.tiedStage = stage;
                uiLevelButton.levelInfoShower = levelInfoShower;
                uiLevelButton.levelSelectorController = this;
            }
        }

        public void LoadStage(StageDef stageToLoad)
        {
            stageToLoad.sceneToLoad.LoadScene();
        }
    }
}