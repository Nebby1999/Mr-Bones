using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nebby.UnityUtils
{
    [Serializable]
    public struct SerializableSystemType
    {

        public class RequiredBaseTypeAttribute : Attribute
        {
            public Type requiredType;
            public RequiredBaseTypeAttribute(Type type)
            {
                requiredType = type;
            }
        }

        public SerializableSystemType(string typeName)
        {
            this._assemblyQualifiedName = "";
            this._assemblyQualifiedName = typeName;
        }
        public SerializableSystemType(Type type)
        {
            this._assemblyQualifiedName = "";
            this.Type = type;
        }

        public string TypeName { get => _assemblyQualifiedName; set => Type = Type.GetType(value); }

        public Type Type
        {
            get
            {
                if (_assemblyQualifiedName == null)
                    return null;

                Type type = Type.GetType(_assemblyQualifiedName);
                if (!(type != null))
                    return null;
                return type;
            }
            set
            {
                _assemblyQualifiedName = ((value != null)) ? value.AssemblyQualifiedName : ""; 
            }
        }

        [SerializeField]
        private string _assemblyQualifiedName;
    }
}