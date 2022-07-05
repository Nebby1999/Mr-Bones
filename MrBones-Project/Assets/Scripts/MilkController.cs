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
        public bool collected = false;
        public float timeTilExit;
        private float internalTimer;
        public void Awake()
        {
            youWinText.SetActive(false);
        }
        public void OnCollect()
        {
            youWinText.SetActive(true);
            collected = true;
        }

        private void Update()
        {
            if(collected)
            {
                internalTimer += Time.deltaTime;
                if(internalTimer > timeTilExit)
                {
                    LoadMainMenu();
                }
            }
        }
        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
