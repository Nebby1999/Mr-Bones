using UnityEngine;
using UnityEditor;
using Nebby.Editor;

namespace Nebby.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(IntMinMax))]
    public class IntMinMaxDrawer : IMGUIPropertyDrawer
    {
        float min, max;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty min = property.FindPropertyRelative("min");
            SerializedProperty max = property.FindPropertyRelative("max");
            SerializedProperty minLimit = property.FindPropertyRelative("minLimit");
            SerializedProperty maxLimit = property.FindPropertyRelative("maxLimit");
            this.min = min.intValue;
            this.max = max.intValue;
            label.tooltip = $"Min: {this.min}\n" +
                $"Max: {this.max}\n" +
                $"MinLimit: {minLimit.floatValue}\n" +
                $"MaxLimit: {maxLimit.floatValue}";

            EditorGUI.BeginProperty(position, label, property);
            Rect minMaxRect = new Rect(position.x, position.y, position.width - 10, position.height);
            EditorGUI.MinMaxSlider(minMaxRect, label, ref this.min, ref this.max, minLimit.floatValue, maxLimit.floatValue);
            min.intValue = Mathf.Min(Mathf.FloorToInt(this.min), min.intValue);
            max.intValue = Mathf.Max(Mathf.CeilToInt(this.max), max.intValue);

            var contextRect = new Rect(minMaxRect.xMax, position.y, 10, position.height);
            EditorGUI.DrawTextureTransparent(contextRect, AssetLocator.ContextMenuIndicator, ScaleMode.ScaleToFit);
            if (Event.current.type == EventType.MouseDown)
            {
                Vector2 mousePos = Event.current.mousePosition;
                if (contextRect.Contains(mousePos))
                {
                    PopupWindow.Show(contextRect, new Windows.IntMinMaxLimitPopup(minLimit, maxLimit, min, max));
                    Event.current.Use();
                }
            }
            EditorGUI.EndProperty();
        }
    }
}
