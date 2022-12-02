using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebby
{
    public abstract class MainGameBehaviour<T> : MonoBehaviour where T : MainGameBehaviour<T>
    {
        public static T Instance { get; protected set; }
        public static bool LoadStarted { get; private set; } = false;
        public static event Action OnLoad;
        public static event Action OnStart;
        public static event Action OnUpdate;
        public static event Action OnFixedUpdate;
        public static event Action OnLateUpdate;
        public static event Action OnShutdown;

        [SerializeField] private SceneReference _gameLoadingSceneName;
        [SerializeField] private SceneReference _loadingFinishedScene;
        [SerializeField] private SceneReference _inbetweenScenesLoadingScene;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if(Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this as T;

            if(!LoadStarted)
            {
                LoadStarted = true;
                SceneLoader.LoadingSceneName = _inbetweenScenesLoadingScene;
                StartCoroutine(LoadGame());
            }
        }

        protected virtual IEnumerator LoadGame()
        {
            LoadStarted = true;
            SceneManager.sceneLoaded += (s, e) =>
            {
                Debug.Log($"Loaded Scene {s.name} loadSceneMode={e}");
            };
            SceneManager.sceneUnloaded += (s) =>
            {
                Debug.Log($"Unloaded Scene {s.name}");
            };
            SceneManager.activeSceneChanged += (os, ns) =>
            {
                Debug.Log($"Active scene changed from {os.name} to {ns.name}");
            };

            //Special loading logic should happen only on runtime, so we're ommiting this when loading from the editor.
            //By ommiting this, we can load any scene and theoretically have entity states and the like running properly.
#if UNITY_EDITOR
#else
            while(SceneManager.GetActiveScene().name != _gameLoadingSceneName.SceneName)
            {
                yield return new WaitForEndOfFrame();
            }
#endif
            yield return new WaitForEndOfFrame();
            yield return LoadGameContent();
            yield return new WaitForEndOfFrame();

            if(OnLoad != null)
            {
                OnLoad();
                OnLoad = null;
            }

            //Special loading logic should happen only on runtime, so we're ommiting this when loading from the editor.
            //By ommiting this, we can load any scene and theoretically have entity states and the like running properly.
#if UNITY_EDITOR
#else
            _loadingFinishedScene.LoadScene();
#endif
        }

        protected abstract IEnumerator LoadGameContent();

        protected virtual void Start()
        {
            OnStart?.Invoke();
        }

        protected virtual void Update()
        {
            OnUpdate?.Invoke();
        }

        protected virtual void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        protected virtual void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
        protected virtual void OnApplicationQuit()
        {
            OnShutdown?.Invoke();
        }
    }
}