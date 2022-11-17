using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MrBones;

namespace MrBones.UI
{
    public class UILevelButton : MonoBehaviour
    {
        public StageDef tiedStage;
        public UILevelInfoShower levelInfoShower;
        public UILevelSelectorController levelSelectorController;

        private TMPro.TextMeshProUGUI textMesh;
        private void Awake()
        {
            textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }

        private void Update()
        {
            if(textMesh && tiedStage)
            {
                textMesh.text = tiedStage.stageNumber.ToString();
            }
        }

        public void SetLevelInfoShower()
        {
            levelInfoShower.currentlyDisplayedStage = tiedStage;
        }

        public void UnsetLevelInfoShower()
        {

            levelInfoShower.currentlyDisplayedStage = null;
        }

        public void OnClicked()
        {
            levelSelectorController.LoadStage(tiedStage);
        }
    }
}