using Nebby;
using UnityEngine;
using TMPro;

namespace MrBones.UI
{
    public class UITimerBehaviour : MonoBehaviour
    {
        public const string formatting = "{0}:{1}.{2}";
        public TimerComponent timerComponent;
        public TextMeshProUGUI textMesh;
        public void Update()
        {
            textMesh.text = string.Format(formatting, timerComponent.MinutesString, timerComponent.SecondsString, timerComponent.DecaSeconds);
        }
    }
}
