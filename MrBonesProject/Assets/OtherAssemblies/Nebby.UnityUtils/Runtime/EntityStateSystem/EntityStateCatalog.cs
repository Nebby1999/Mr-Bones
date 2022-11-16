using EntityStates;
using Nebby;
using Nebby.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public static class EntityStateCatalog
    {
        public static bool Initialized { get; private set; }
        private static Type[] entityStates = Array.Empty<Type>();
        private static readonly Dictionary<Type, Action<object>> instanceFieldInitializers = new Dictionary<Type, Action<object>>();

        public static void Initialize()
        {
            if (Initialized)
                return;

            Initialized = true;

            entityStates = LoadEntityStates();

            var entityStateConfigurations = Resources.LoadAll<EntityStateConfiguration>("ESC");
            foreach(EntityStateConfiguration config in entityStateConfigurations)
            {
                ApplyStateConfig(config);
            }
        }

        private static Type[] LoadEntityStates()
        {
            List<Type> entityStates = new List<Type>();
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    Type[] typestates = assembly.GetTypesSafe()
                        .Where(t => t.IsSubclassOf(typeof(EntityStates.EntityStateBase)))
                        .Where(t => !t.IsAbstract)
                        .ToArray();
                    entityStates.AddRange(typestates);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            return entityStates.ToArray();
        }

        private static void ApplyStateConfig(EntityStateConfiguration config)
        {
            Type targetType = config.targetType.Type;
            if (targetType == null)
                return;

            if (!entityStates.Contains(targetType))
                return;

            config.ApplyStaticConfiguration();
            Action<object> instanceInitializer = config.CreateInstanceInitializer();
            if(instanceInitializer == null)
            {
                instanceFieldInitializers.Remove(targetType);
            }
            else
            {
                instanceFieldInitializers[targetType] = instanceInitializer;
            }
        }

        public static EntityStateBase InstantiateState(SerializableSystemType serializableSystemType)
        {
            if (serializableSystemType.Type == null || !serializableSystemType.Type.IsSubclassOf(typeof(EntityStateBase)))
                throw new ArgumentException($"SerializableSystemType provided has a null type or does not subclass EntityState");
            return InstantiateState(serializableSystemType.Type);
        }
        public static EntityStateBase InstantiateState(Type stateType)
        {
            if(stateType != null && stateType.IsSubclassOf(typeof(EntityStateBase)))
            {
                return Activator.CreateInstance(stateType) as EntityStateBase;
            }
            Debug.LogError($"State provided is either null or does not inherit from EntityState (State: {stateType}");
            return null;
        }
        public static void InitializeStateField(EntityStateBase entityState)
        {
            if(instanceFieldInitializers.TryGetValue(entityState.GetType(), out Action<object> initializer))
            {
                initializer(entityState);
            }
        }
    }
}