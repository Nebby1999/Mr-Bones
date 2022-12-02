using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.Serialization;
using System.Reflection;
using System;
using EntityStates;

namespace Nebby
{
    [CreateAssetMenu(fileName = "new EntityStateConfiguration", menuName = "Nebby/EntityStateConfiguration")]
    public class EntityStateConfiguration : ScriptableObject
    {
        [SerializableSystemType.RequiredBaseType(typeof(EntityStates.EntityStateBase))]
        public SerializableSystemType targetType;
        public SerializedFieldCollection fieldCollection;

        [ContextMenu("Set name to targetType name")]
        public void SetNameToTargetTypeName()
        {
            if (targetType.Type == null)
                return;

            name = targetType.Type.FullName;
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.RenameAsset(UnityEditor.AssetDatabase.GetAssetPath(this), targetType.Type.FullName);
#endif
        }

#if UNITY_EDITOR
        [ContextMenu("Editor PlayMode-Test serialized values")]
        public void RuntimeTestSerializedValues()
        {
            if (!Application.isPlaying)
                return;

            if (targetType.Type == null)
                return;

            EntityStateBase state = EntityStateCatalog.InstantiateState(targetType);
            foreach(FieldInfo field in state.GetType().GetFields())
            {
                Debug.Log($"{field.Name} ({field.FieldType.Name}): {field.GetValue(state)}");
            }
        }
#endif

        public void ApplyStaticConfiguration()
        {
            if (!Application.isPlaying)
                return;

            if(targetType.Type == null)
            {
                Debug.LogError($"{this} has an invalid TargetType set! (targetType.TypeName value: {targetType.TypeName})");
                return;
            }

            foreach(SerializedField field in fieldCollection.serializedFields)
            {
                try
                {
                    FieldInfo fieldInfo = targetType.Type.GetField(field.fieldName, BindingFlags.Public | BindingFlags.Static);
                    if (fieldInfo == null)
                    {
                        continue;
                    }

                    var serializedValueForfield = field.serializedValue.GetValue(fieldInfo);
                    if (serializedValueForfield != null)
                    {
                        fieldInfo.SetValue(fieldInfo, serializedValueForfield);
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public Action<object> CreateInstanceInitializer()
        {
            if (!Application.isPlaying)
                return null;

            if(targetType.Type == null)
            {
                Debug.LogError($"{this} has an invalid TargetType set! (targetType.TypeName value: {targetType.TypeName})");
                return null;
            }

            List<(FieldInfo, object)> fieldValuePair = new List<(FieldInfo, object)>();
            foreach(SerializedField serializedField in fieldCollection.serializedFields)
            {
                try
                {
                    FieldInfo field = targetType.Type.GetField(serializedField.fieldName);
                    if(field != null && ShouldSerializeField(field))
                    {
                        fieldValuePair.Add((field, serializedField.serializedValue.GetValue(field)));
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                }
            }

            if (fieldValuePair.Count == 0)
                return null;

            return InitializeObject;
            bool ShouldSerializeField(FieldInfo field)
            {
                var hasSerializeField = field.GetCustomAttribute<SerializeField>() != null;
                var hasHideInInspector = field.GetCustomAttribute<HideInInspector>() != null;

                return hasSerializeField && !hasHideInInspector;
            }

            void InitializeObject(object obj)
            {
                foreach(var pair in fieldValuePair)
                {
                    pair.Item1.SetValue(obj, pair.Item2);
                }
            }
        }

        public void Awake()
        {
            fieldCollection.PurgeUnityPseudoNull();
        }
    }
}