using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebby
{
    [Serializable]
    public class SceneReference : IEqualityComparer<SceneReference>, IEquatable<SceneReference>
    {
        public string SceneName => _sceneName;
        [SerializeField] private string _sceneName;
        public string ScenePath => _scenePath;
        [SerializeField] private string _scenePath;
        public int SceneBuildIndex => _sceneBuildIndex;
        [SerializeField] private int _sceneBuildIndex = -1;

        public Scene GetScene()
        {
            var sceneThatMayOrMayNotBeValid = SceneManager.GetSceneByBuildIndex(_sceneBuildIndex);
            if (sceneThatMayOrMayNotBeValid.IsValid())
            {
                return sceneThatMayOrMayNotBeValid;
            }
            Debug.LogError($"SceneReference {this} represents an invalid scene.");
            return sceneThatMayOrMayNotBeValid;
        }

        public Scene LoadScene() => LoadScene(LoadSceneMode.Single);
        public Scene LoadScene(LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadScene(_scenePath, new LoadSceneParameters(loadSceneMode));
        }

        public AsyncOperation LoadSceneAsync() => LoadSceneAsync(LoadSceneMode.Single);
        public AsyncOperation LoadSceneAsync(LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(_scenePath, loadSceneMode);
        }

        public override string ToString()
        {
            return $"{_sceneName} ({_scenePath})({_sceneBuildIndex})";
        }

        public bool Equals(SceneReference x, SceneReference y)
        {
            return x.SceneBuildIndex == y.SceneBuildIndex;
        }

        public int GetHashCode(SceneReference obj)
        {
            return _scenePath.GetHashCode();
        }

        public bool Equals(SceneReference other)
        {
            return SceneBuildIndex == other.SceneBuildIndex;
        }
    }
}
