using Nebby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    public class CharacterBody : MonoBehaviour
    {
        public BodyStats bodyStats;
        [LangToken] public string bodyName;

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public float Speed { get; private set; }
        public int JumpCount { get; private set; }

        public GameObject SpiritObject
        {
            get => _spiritObject;
            set
            {
                if(_spiritObject != value)
                {
                    _spiritObject = value;
                    _spirit = _spiritObject.GetComponent<CharacterSpirit>();
                }
            }
        }
        private GameObject _spiritObject;

        public CharacterSpirit Spirit
        {
            get
            {
                if(!SpiritObject)
                {
                    return null;
                }
                return _spirit;
            }
        }
        private CharacterSpirit _spirit;

        public InputSimulator InputSimulator { get; private set; }

        private void Awake()
        {
            InputSimulator = GetComponent<InputSimulator>();
            SetStats();
        }

        private void SetStats()
        {
            MaxHealth = bodyStats.health;
            Speed = bodyStats.speed;
            JumpCount = bodyStats.jumpCount;
        }
    }
}
