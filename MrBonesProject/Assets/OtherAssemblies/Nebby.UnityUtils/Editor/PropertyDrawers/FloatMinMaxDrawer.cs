using UnityEngine;
using UnityEditor;
using Nebby.Editor;

namespace Nebby.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(FloatMinMax))]
    public class FloatMinMaxDrawer : IMGUIPropertyDrawer
    {
        float min, max;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty min = property.FindPropertyRelative("min");
            SerializedProperty max = property.FindPropertyRelative("max");
            SerializedProperty minLimit = property.FindPropertyRelative("minLimit");
            SerializedProperty maxLimit = property.FindPropertyRelative("maxLimit");
            this.min = min.floatValue;
            this.max = max.floatValue;
            label.tooltip = $"Min: {this.min}\n" +
                $"Max: {this.max}\n" +
                $"MinLimit: {minLimit.floatValue}\n" +
                $"MaxLimit: {maxLimit.floatValue}";

            EditorGUI.BeginProperty(position, label, property);
            Rect minMaxRect = new Rect(position.x, position.y, position.width - 10, position.height);
            EditorGUI.MinMaxSlider(minMaxRect, label, ref this.min, ref this.max, minLimit.floatValue, maxLimit.floatValue);
            min.floatValue = Mathf.Max(this.min, minLimit.floatValue);
            max.floatValue = Mathf.Min(this.max, maxLimit.floatValue);

            var contextRect = new Rect(minMaxRect.xMax, position.y, 10, position.height);
            EditorGUI.DrawTextureTransparent(contextRect, AssetLocator.ContextMenuIndicator, ScaleMode.ScaleToFit);
            if (Event.current.type == EventType.MouseDown)
            {
                Vector2 mousePos = Event.current.mousePosition;
                if (contextRect.Contains(mousePos))
                {
                    PopupWindow.Show(contextRect, new Windows.FloatMinMaxLimitPopup(minLimit, maxLimit, min, max));
                    Event.current.Use();
                }
            }
            EditorGUI.EndProperty();
        }
    }
}
