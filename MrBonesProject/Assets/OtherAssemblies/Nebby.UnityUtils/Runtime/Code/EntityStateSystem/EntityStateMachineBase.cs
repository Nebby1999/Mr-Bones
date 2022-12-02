using EntityStates;
using Nebby.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public abstract class EntityStateMachineBase : MonoBehaviour
    {
        public string stateMachineName;

        [SerializableSystemType.RequiredBaseType(typeof(EntityStateBase))]
        public SerializableSystemType initialState;
        [SerializableSystemType.RequiredBaseType(typeof(EntityStateBase))]
        public SerializableSystemType mainState;

        public EntityStateBase NewState { get; private set; }
        public EntityStateBase CurrentState { get; private set; }

        protected virtual void Awake()
        {
            CurrentState = new Uninitialized();
            CurrentState.outer = this;
        }

        protected virtual void Start()
        {
            var initState = initialState.Type;
            if(CurrentState is Uninitialized && initState != null && initState.IsSubclassOf(typeof(EntityStateBase)))
            {
                SetState(EntityStateCatalog.InstantiateState(initState));
            }
        }

        protected virtual void SetState(EntityStateBase newState)
        {
            if (newState == null)
                throw new NullReferenceException("newState is null");

            newState.outer = this;
            NewState = null;
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public virtual void SetNextState(EntityStateBase newNextEntityState)
        {
            newNextEntityState.outer = this;
            NewState = newNextEntityState;
        }

        public virtual void SetNextStateToMain()
        {
            SetNextState(EntityStateCatalog.InstantiateState(mainState));
        }

        protected virtual void FixedUpdate()
        {
            CurrentState.FixedUpdate();

            if (NewState != null)
                SetState(NewState);
        }

        protected virtual void Update()
        {
            CurrentState.Update();
        }

        protected virtual void OnDestroy()
        {
            CurrentState.OnExit();
        }
    }
}