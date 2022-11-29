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
            if(StageController.Instance)
            {
                StageController.Instance.Restart();
                PauseManager.UnpauseGame();
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
            PauseManager.UnpauseGame();
        }
    }
}
