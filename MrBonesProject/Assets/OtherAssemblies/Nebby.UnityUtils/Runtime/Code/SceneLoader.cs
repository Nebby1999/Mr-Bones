using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System;

namespace Nebby
{
    public static class SceneLoader
    {
        private static DummyLoader Loader
        {
            get
            {
                if (!_loader)
                {
                    _loader = new GameObject("DummySceneLoader").AddComponent<DummyLoader>();
                }
                return _loader;
            }
        }
        private static DummyLoader _loader;

        public static SceneReference LoadingSceneName
        {
            get
            {
                return _loadingSceneName;
            }
            set
            {
                StackTrace stackTrace = new StackTrace();
                var callerFrame = stackTrace.GetFrame(1);

                var type = typeof(MainGameBehaviour<>);
                type = type.GetGenericTypeDefinition();

                if (!IsTypeDerivedFromGenericType(callerFrame.GetMethod().DeclaringType, typeof(MainGameBehaviour<>)))
                {
                    throw new InvalidOperationException("Loading Scene Name can only be set by a class inheriting from MainGameBehaviour<T>");
                }
                _loadingSceneName = value;
            }
        }

        public static bool IsTypeDerivedFromGenericType(Type typeToCheck, Type genericType)
        {
            if (typeToCheck == typeof(object))
            {
                return false;
            }
            else if (typeToCheck == null)
            {
                return false;
            }
            else if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            else
            {
                return IsTypeDerivedFromGenericType(typeToCheck.BaseType, genericType);
            }
        }
        private static SceneReference _loadingSceneName;

        private class DummyLoader : MonoBehaviour { }

        public static void LoadScene(string sceneName) => LoadScene(new SceneReference(sceneName));
        public static void LoadScene(SceneReference sceneRef)
        {
            sceneRef.LoadScene();
        }

        public static void LoadSceneAsync(string sceneName) => LoadSceneAsync(new SceneReference(sceneName));

        public static void LoadSceneAsync(SceneReference sceneRef)
        {

        }
    }
}