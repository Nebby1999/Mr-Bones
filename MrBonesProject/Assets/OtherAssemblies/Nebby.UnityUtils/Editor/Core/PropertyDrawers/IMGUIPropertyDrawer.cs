using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor
{
    public abstract class IMGUIPropertyDrawer : PropertyDrawer
    {
        public float StandardPropertyHeight => EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        protected SerializedProperty[] GetChildProperties(SerializedProperty prop)
        {
            var list = new List<SerializedProperty>();
            foreach(SerializedProperty p in prop)
            {
                list.Add(p);
            }
            return list.ToArray();
        }
        protected float GetPropertyHeightFromFoldout(ref SerializedProperty foldoutProperty)
        {
            float height = StandardPropertyHeight;
            var copy = foldoutProperty.Copy();
            var propCount = foldoutProperty.CountInProperty() - 1;
            foldoutProperty = copy;

            if(foldoutProperty.isExpanded)
            {
                height += StandardPropertyHeight * propCount;
            }
            return height;
        }
    }
}