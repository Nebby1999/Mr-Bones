using System;
using UnityEditor;
using UnityEngine;

namespace Nebby
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T: SingletonBehaviour<T>
    {
        public static T Instance { get; protected set; }
        public virtual bool DestroyIfDuplicate { get; } = false;
        public virtual void OnEnable()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"An instance of the singleton {typeof(T).Name} already exists! " +
                    $"Only a single instance should exist at a time! " +
                    (DestroyIfDuplicate ? "Destroying Duplicate" : $"Replacing instance with new one."));

                if (DestroyIfDuplicate)
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
            Instance = this as T;
        }

        public virtual void OnDisable()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}