using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UIElements;

namespace Nebby.Editor
{
    public abstract class VisualElementEditorWindow : ExtendedEditorWindow
    {
        protected override void OnWindowOpened()
        {
            base.OnWindowOpened();
            rootVisualElement.Bind(SerializedObject);
            FinalizeUI();
        }

        protected abstract void FinalizeUI();

        protected virtual void CreateGUI()
        {
            rootVisualElement.Clear();
            TemplateHelpers.GetTemplateInstance(GetType().Name, rootVisualElement);
        }
    }
}