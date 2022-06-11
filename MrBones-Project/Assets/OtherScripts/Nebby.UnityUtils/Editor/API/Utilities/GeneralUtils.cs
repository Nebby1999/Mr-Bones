using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Nebby.UnityUtils.Editor.Utils
{
    public static class GeneralUtils
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
    }
}
