using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones.UI
{
    public class UILevelInfoShower : MonoBehaviour
    {
        public Stages.StageDef currentlyDisplayedStage;
        public TMPro.TextMeshProUGUI textMesh;

        private void Update()
        {
            if(!currentlyDisplayedStage)
            {
                textMesh.text = String.Empty;
                return;
            }

            textMesh.text = $"{currentlyDisplayedStage.stageName}";
        }
    }
}