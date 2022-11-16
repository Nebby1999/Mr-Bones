using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones.UI
{
    public class UISpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private Canvas targetCanvas;
        [SerializeField] private bool spawnOnAwake;
        [SerializeField] private UnityEngine.Events.UnityEvent onUISpawned;

        private void Awake()
        {
            if(spawnOnAwake)
            {
                SpawnUI();
            }
        }

        public void SpawnUI()
        {
            if(!targetCanvas)
            {
                targetCanvas = UIMainBehaviour.Instance.MainCanvas;
            }

            Instantiate(prefabToSpawn, targetCanvas.transform);
            onUISpawned?.Invoke();
        }
    }
}
