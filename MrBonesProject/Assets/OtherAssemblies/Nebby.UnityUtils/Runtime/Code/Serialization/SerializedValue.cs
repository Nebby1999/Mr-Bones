using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Nebby.Serialization
{
    [Serializable]
    public struct SerializedValue : IEquatable<SerializedValue>
    {
        public string stringValue;
        public UnityEngine.Object objectReferenceValue;

        public bool Equals(SerializedValue other)
        {
            if (string.Equals(stringValue, other.stringValue, StringComparison.Ordinal))
                return objectReferenceValue == other.objectReferenceValue;
            return false;
        }

        public object GetValue(FieldInfo fieldInfo)
        {
            Type fieldType = fieldInfo.FieldType;
            if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType))
            {
                if ((object)objectReferenceValue != null)
                {
                    Type type = objectReferenceValue.GetType();
                    if (!fieldType.IsAssignableFrom(type))
                    {
                        if (type == typeof(UnityEngine.Object))
                        {
                            return null;
                        }
                        throw new Exception($"Value \"{objectReferenceValue}\" of type \"{type}\" is not suitable for field \"{fieldType.Name} {fieldInfo.DeclaringType.Name}.{fieldInfo.Name}\"");
                    }
                }
                return objectReferenceValue;
            }
            if (stringValue != null)
            {
                if(StringSerializer.CanSerializeType(fieldType))
                {
                    try
                    {
                        return StringSerializer.Deserialize(fieldType, stringValue);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarningFormat("Could not deserialize field '{0}.{1}': {2}", fieldInfo.DeclaringType.Name, fieldInfo.Name, e);
                    }
                }
            }

            if (fieldType.IsValueType)
                return Activator.CreateInstance(fieldType);

            return null;
        }

        public void SetValue(FieldInfo fieldInfo, object newValue)
        {
            try
            {
                stringValue = null;
                objectReferenceValue = null;
                Type fieldType = fieldInfo.FieldType;
                if(typeof(Object).IsAssignableFrom(fieldType))
                {
                    objectReferenceValue = (Object)newValue;
                    return;
                }
                if(StringSerializer.CanSerializeType(fieldType))
                {
                    stringValue = StringSerializer.Serialize(fieldType, newValue);
                    return;
                }
                throw new Exception($"Unrecognized type {fieldType.FullName}.");
            }
            catch(Exception exception)
            {
                throw new Exception($"Could not serialize field \"{fieldInfo.DeclaringType.FullName}.{fieldInfo.Name}\"", exception);
            }
        }

        public void PurgeUnityNull()
        {
            if(objectReferenceValue != null && !objectReferenceValue)
            {
                objectReferenceValue = null;
            }
        }

        public static bool CanSerializeField(FieldInfo fieldInfo)
        {
            Type fieldType = fieldInfo.FieldType;
            if (!typeof(Object).IsAssignableFrom(fieldType) && !StringSerializer.CanSerializeType(fieldType))
                return false;

            if (fieldInfo.IsStatic && fieldInfo.IsPublic)
                return true;

            bool serializeField = fieldInfo.GetCustomAttribute<SerializeField>() != null;
            bool hideInInspector = fieldInfo.GetCustomAttribute<HideInInspector>() != null;

            return serializeField && !hideInInspector;
        }
    }
}