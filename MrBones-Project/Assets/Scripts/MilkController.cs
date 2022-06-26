using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrBones
{
    public class MilkController : MonoBehaviour
    {
        public GameObject youWinText;
        public void Awake()
        {
            youWinText.SetActive(false);
        }
        public void OnCollect()
        {
            youWinText.SetActive(true);
            Invoke(nameof(LoadMainMenu), 3);
        }
        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
