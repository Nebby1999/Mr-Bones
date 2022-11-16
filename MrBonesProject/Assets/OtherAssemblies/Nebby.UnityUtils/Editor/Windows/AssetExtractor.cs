using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using System.IO;

namespace Nebby.Editor.Windows
{
    public class AssetExtractor : IMGUIEditorWindow
    {
        public Object parentObject;
        public Dictionary<Object, bool> objectToShouldExtract = new Dictionary<Object, bool>();
        private (Object, GUIContent)[] subAssetsOfParentObject = Array.Empty<(Object, GUIContent)>();

        [MenuItem("NebbyUtils/AssetEmbedding/ExtractAsset")]
        private static void OpenWindow()
        {
            var window = OpenEditorWindow<AssetExtractor>();
            window.maxSize = new Vector2(300f, 1000000000);
            window.Focus();
        }

        protected override void OnGUI()
        {
            EditorGUILayout.BeginVertical("box");

            IMGUIUtil.DrawCheckableProperty(GetProp("parentObject"), GetSubAssets);

            if (parentObject == null)
            {
                EditorGUILayout.HelpBox("Please select your parent object.", MessageType.Info);
            }

            DrawAssetsToExtract();

            EditorGUILayout.HelpBox("Extracting a subAsset from this parent asset will not update references from the subAsset to the new Asset.", MessageType.Warning);

            if (IMGUIUtil.ConditionalButton(ShouldBeginExtraction, "Extract Assets"))
            {
                ExtractAssets();
            }
            EditorGUILayout.EndVertical();
        }

        private bool ShouldBeginExtraction()
        {
            if (!parentObject)
                return false;

            bool notSubAsset = !AssetDatabase.IsSubAsset(parentObject);
            bool hasSubAssets = objectToShouldExtract.Count > 0;

            return notSubAsset && hasSubAssets;
        }

        private void GetSubAssets()
        {
            var subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(parentObject));

            List<(Object, GUIContent)> subAssetGUIContentPair = new();
            objectToShouldExtract.Clear();
            foreach(var asset in subAssets)
            {
                objectToShouldExtract.Add(asset, false);
                subAssetGUIContentPair.Add((asset, new GUIContent
                {
                    text = $"{asset.name}",
                    tooltip = $"Should this asset be extracted",
                    image = AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(asset))
                }));
            }
            subAssetsOfParentObject = subAssetGUIContentPair.ToArray();
        }
        private void DrawAssetsToExtract()
        {
            if (!parentObject)
                return;

            if(AssetDatabase.IsSubAsset(parentObject))
            {
                EditorGUILayout.HelpBox($"The selected Parent Object is not the root asset, but its a sub asset of {AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(parentObject))}", MessageType.Info);
                if(GUILayout.Button("Select Parent"))
                {
                    parentObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(parentObject));
                    SerializedObject.Update();
                    GetSubAssets();
                }
                return;
            }
            if(parentObject && subAssetsOfParentObject.Length == 0)
            {
                EditorGUILayout.HelpBox("Selected Parent Object has no Sub Assets", MessageType.Info);
                return;
            }
            foreach(var subAsset in subAssetsOfParentObject)
            {
                bool newVal = EditorGUILayout.Toggle(subAsset.Item2, objectToShouldExtract[subAsset.Item1]);
                objectToShouldExtract[subAsset.Item1] = newVal;
            }
        }

        private void ExtractAssets()
        {
            AssetDatabase.StartAssetEditing();
            var parentObjectDirectory = IOUtils.GetDirectoryOfObject(parentObject);
            foreach(var (obj, shouldExtract) in objectToShouldExtract)
            {
                try
                {
                    if(shouldExtract)
                    {
                        AssetDatabase.RemoveObjectFromAsset(obj);
                        AssetDatabase.CreateAsset(obj, IOUtils.FormatPathForUnity(Path.Combine(parentObjectDirectory, $"{obj.name}.asset")));
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError($"Could not extract {obj} from {parentObject}\n{e}");
                }
            }
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parentObject));
            AssetDatabase.StopAssetEditing();

            Close();
        }
    }
}
