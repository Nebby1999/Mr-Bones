using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Nebby.Editor
{
    public static class AssetLocator
    {
        public const string contextMenuGUID = "f3313ce1861c6094a825e4849287951a";
        public static Texture2D ContextMenuIndicator { get => AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(contextMenuGUID)); }
    }
}