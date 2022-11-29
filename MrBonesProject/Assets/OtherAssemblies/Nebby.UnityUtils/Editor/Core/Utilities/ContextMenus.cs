using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;

namespace Nebby.Editor
{
    public static class ContextMenus
    {
        [MenuItem("Assets/Copy Guid")]
        public static void GetGUID()
        {
            if (!Selection.activeObject)
                return;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var guid = AssetDatabase.AssetPathToGUID(path);
            UnityEngine.GUIUtility.systemCopyBuffer = guid;
        }
        [MenuItem("CONTEXT/ShadowCaster2D/Set Shape to Polygon Collider", true)]
        public static bool Validate_ShadowCaster2DSetShapeToPolygonCollider(MenuCommand command) => ((Component)command.context).GetComponent<PolygonCollider2D>();
        [MenuItem("CONTEXT/ShadowCaster2D/Set Shape to Polygon Collider")]
        public static void ShadowCaster2D_SetShapeToPolygonCollider(MenuCommand command)
        {
            ShadowCaster2D caster = (ShadowCaster2D)command.context;
            PolygonCollider2D collider = caster.GetComponent<PolygonCollider2D>();

            var field = typeof(ShadowCaster2D).GetField("m_ShapePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(caster, collider.points.Select(v => (Vector3)v).ToArray());
        }

        [MenuItem("CONTEXT/Light2D/Set Shape to Polygon Collider", true)]
        public static bool Validate_Light2D_SetShapeToPolygonCollider(MenuCommand command)
        {
            var light2D = ((Light2D)command.context);
            var polygonCollider = light2D.GetComponent<PolygonCollider2D>();

            return light2D.lightType == Light2D.LightType.Freeform && polygonCollider;
        }
        [MenuItem("CONTEXT/Light2D/Set Shape to Polygon Collider")]
        public static void Light2D_SetShapeToPolygonCollider(MenuCommand command)
        {
            Light2D caster = (Light2D)command.context;
            PolygonCollider2D collider = caster.GetComponent<PolygonCollider2D>();

            var field = typeof(Light2D).GetField("m_ShapePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(caster, collider.points.Select(v => (Vector3)v).ToArray());
        }
    }
}
