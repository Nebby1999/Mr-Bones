using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBones.UI
{
    public class MainMenuController : UnityEngine.MonoBehaviour
    {
        public void LoadMainLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level0");
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
