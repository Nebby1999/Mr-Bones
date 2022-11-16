using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    public class CharacterSpirit : MonoBehaviour
    {
        [SerializeField] private GameObject characterBodyPrefab;
        public bool spawnOnStart;

        public GameObject BodyObjectInstance { get; private set; }
        public CharacterBody CharacterBodyInstance => BodyObjectInstance ? BodyObjectInstance.GetComponent<CharacterBody>() : null;
        public event Action<CharacterBody> OnBodySpawned;

        private void Start()
        {
            if(spawnOnStart)
            {
                SpawnHere();
            }
        }

        public void SpawnHere() => Spawn(transform.position, transform.rotation);
        public void Spawn(Vector3 position, Quaternion rotation)
        {
            if(!characterBodyPrefab || !characterBodyPrefab.GetComponent<CharacterBody>())
            {
                Debug.LogError($"Provided CharacterBody prefab in {this} is not a CharacterBody prefab.");
                return;
            }
            if(BodyObjectInstance)
            {
                Debug.LogError("This spirit already has a body, spirits can only have one body.");
                return;
            }
            BodyObjectInstance = Instantiate(characterBodyPrefab, position, rotation);
            CharacterBodyInstance.SpiritObject = gameObject;
            OnBodySpawned?.Invoke(CharacterBodyInstance);
        }
        public void OnValidate()
        {
            if(characterBodyPrefab && !characterBodyPrefab.GetComponent<CharacterBody>())
            {
                Debug.LogError($"Provided CharacterBody prefab in {this} is not a CharacterBody prefab.");
                characterBodyPrefab = null;
            }
        }
    }
}
