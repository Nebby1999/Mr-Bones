using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Nebby.UnityUtils.Editor.Utils;

namespace Nebby.UnityUtils.Editor.Core.Inspectors
{
    public abstract class IMGUIToVisualElementInspector<TObject> : UnityEditor.Editor where TObject : UnityEngine.Object
    {
        protected VisualElement RootVisualElement
        {
            get
            {
                if (_visualElement == null)
                {
                    _visualElement = new VisualElement();
                    _visualElement.name = $"{GetType().Name}_RootElement";
                }
                return _visualElement;
            }
        }
        private VisualElement _visualElement;

        protected TObject TargetType => target as TObject;

        /// <summary>
        /// Cannot be overwritten, creates the inspector gui using the serialized object's visible children and property fields
        /// <para>If you want to draw extra visual elements, write them in the <see cref="FinishGUI"/> method</para>
        /// </summary>
        /// <returns>The <see cref="RootVisualElement"/> property</returns>
        public sealed override VisualElement CreateInspectorGUI()
        {
            var children = serializedObject.GetVisibleChildren();
            foreach (var child in children)
            {
                if (child.name == "m_Script")
                {
                    ObjectField objField = new ObjectField();
                    objField.SetObjectType<MonoScript>();
                    objField.value = child.objectReferenceValue;
                    objField.label = child.displayName;
                    objField.name = child.name;
                    objField.bindingPath = child.propertyPath;
                    objField.SetEnabled(false);
                    RootVisualElement.Add(objField);
                    continue;
                }

                PropertyField propField = new PropertyField(child);
                propField.name = child.name;
                RootVisualElement.Add(new PropertyField(child));
            }
            FinishGUI();
            return RootVisualElement;
        }

        /// <summary>
        /// Override this method to finish the implementation of your GUI by modifying the RootVisualElement
        /// </summary>
        protected virtual void FinishGUI() { }
    }
}
