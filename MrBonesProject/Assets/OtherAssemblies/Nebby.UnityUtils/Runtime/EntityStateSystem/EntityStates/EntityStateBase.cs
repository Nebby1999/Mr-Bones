using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Nebby;

namespace EntityStates
{
    public abstract class EntityStateBase
    {
        public EntityStateBase()
        {
            EntityStateCatalog.InitializeStateField(this);
        }
        public EntityStateMachineBase outer;

        protected float FixedAge { get => _fixedAge; set => _fixedAge = value; }
        private float _fixedAge;
        protected float Age { get => _age; set => _age = value; }
        private float _age;
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Update()
        {
            Age += Time.deltaTime;
        }
        public virtual void FixedUpdate()
        {
            FixedAge += Time.fixedDeltaTime;
        }

        protected static void Destroy(UnityEngine.Object obj) => UnityEngine.Object.Destroy(obj);
        protected T GetComponent<T>() => outer.GetComponent<T>();
        protected Component GetComponent(Type type) => outer.GetComponent(type);
        protected Component GetComponent(string name) => outer.GetComponent(name);
    }
}
