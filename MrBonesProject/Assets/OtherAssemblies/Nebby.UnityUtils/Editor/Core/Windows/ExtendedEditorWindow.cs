using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Nebby.Editor
{
    public abstract class ExtendedEditorWindow : UnityEditor.EditorWindow
    {
        protected SerializedObject SerializedObject { get; set; }

        public static TEditorWindow OpenEditorWindow<TEditorWindow>(string windowName = null) where TEditorWindow : ExtendedEditorWindow
        {
            TEditorWindow window = GetWindow<TEditorWindow>(windowName ?? ObjectNames.NicifyVariableName(typeof(TEditorWindow).Name));
            window.SerializedObject = new SerializedObject(window);
            window.OnWindowOpened();
            return window;
        }

        protected virtual void OnWindowOpened()
        {

        }

        protected virtual void OnEnable()
        {

        }
    }
}
