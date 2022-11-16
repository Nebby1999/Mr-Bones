using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Nebby.Editor.Windows
{
    /*public class EmbeddedAssetRenamer : IMGUIEditorWindow
    {
        public Object parentObject;
        public Dictionary<Object, string> objectToNewName = new Dictionary<Object, string>();
        private (Object, GUIContent)[] subAssetsOfParentObject = Array.Empty<(Object, GUIContent)>();

        [MenuItem("NebbyUtils/AssetEmbedding/RenameEmbeddedAsset")]
        private static void OpenWindow()
        {
            var window = OpenEditorWindow<EmbeddedAssetRenamer>();
            window.maxSize = new Vector2(300f, 10000000f);
            window.Focus();
        }

        protected override void OnGUI()
        {
            EditorGUILayout.BeginVertical("box");

            IMGUIUtil.DrawCheckableProperty(GetProp("parentObject"), GetSubAssets);

            if(parentObject == null)
            {
                EditorGUILayout.HelpBox("Please select your parent object.", MessageType.Info);
            }

            DrawAssetsToRename();

            if(IMGUIUtil.ConditionalButton(ShouldBeginRenaming, "Rename Assets"))
            {
                RenameAssets();
            }
            EditorGUILayout.EndVertical();
        }

        private bool ShouldBeginRenaming()
        {
            if (!parentObject)
                return false;

            bool notSubASset = !AssetDatabase.IsSubAsset(parentObject);
            bool hasSubAssets = objectToNewName.Count > 0;

            return notSubASset && hasSubAssets;
        }

        private void GetSubAssets()
        {
            var subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(parentObject));

            List<(Object, GUIContent)> subAssetGUIContentPair = new();
            objectToNewName.Clear();
            foreach(var asset in subAssets)
            {
                objectToNewName.Add(asset, string.Empty);
                subAssetGUIContentPair.Add((asset, new GUIContent
                {
                    text = $"{asset.name}",
                    image = AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(asset))
                }));
            }

            subAssetsOfParentObject = subAssetGUIContentPair.ToArray();
        }

        private void DrawAssetsToRename()
        {
            if (!parentObject)
                return;

            if (AssetDatabase.IsSubAsset(parentObject))
            {
                EditorGUILayout.HelpBox($"The selected Parent Object is not the root asset, but its a sub asset of {AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(parentObject))}", MessageType.Info);
                if (GUILayout.Button("Select Parent"))
                {
                    parentObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(parentObject));
                    SerializedObject.Update();
                    GetSubAssets();
                }
                return;
            }
            if (parentObject && subAssetsOfParentObject.Length == 0)
            {
                EditorGUILayout.HelpBox("Selected Parent Object has no Sub Assets", MessageType.Info);
                return;
            }
            foreach (var subAsset in subAssetsOfParentObject)
            {
                string newVal = EditorGUILayout.TextField(subAsset.Item2, objectToNewName[subAsset.Item1]);
                objectToNewName[subAsset.Item1] = newVal;
            }
            EditorGUILayout.HelpBox("Leaving the text field empty causes the asset to be skipped.", MessageType.Info);
        }

        private void RenameAssets()
        {
            AssetDatabase.StartAssetEditing();
            foreach(var (obj, newName) in objectToNewName)
            {
                try
                {
                    if(newName.IsNullOrEmptyOrWhitespace())
                    {
                        continue;
                    }
                    AssetDatabaseUtils.RenameSubAsset(obj, parentObject, newName);
                }
                catch(Exception e)
                {
                    Debug.LogError($"Could not rename {obj} inside {parentObject}\n {e}");
                }
            }
            AssetDatabase.StopAssetEditing();
            Close();
        }
    }*/
}
