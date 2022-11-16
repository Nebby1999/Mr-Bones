using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebby
{
    [RequireComponent(typeof(Collider2D))]
    public class SceneRestarter : MonoBehaviour
    {
        public TagObject requiredTag;
        private string sceneName;
        private void Awake()
        {
            sceneName = SceneManager.GetActiveScene().name;       
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject.GetRootGameObject();
            var tagContainer = go.GetComponent<TagContainer>();
            if(tagContainer)
            {
                if(tagContainer.ObjectHasTag(requiredTag))
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}