using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby;

namespace MrBones.UI
{
    public class UIPauseMenu : MonoBehaviour
    {
        public void Unpause()
        {
            PauseManager.UnpauseGame();
        }

        public void RestartStage()
        {
            if(Stages.StageController.Instance)
            {
                Stages.StageController.Instance.Restart();
                return;
            }
            Debug.LogError("No stage controller instance");
        }

        public void ShowOptions()
        {

        }
    
        public void Quit()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
        }
    }
}
