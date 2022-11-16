using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;

namespace Nebby.Editor.Windows
{
    public class AssetEmbedder : IMGUIEditorWindow
    {
        public Object parentObject;
        public List<Object> assetsToEmbed = new List<Object>();
        public bool deleteOriginalAssets;

        [MenuItem("NebbyUtils/AssetEmbedding/EmbedAsset")]
        private static void OpenWindow()
        {
            var window = OpenEditorWindow<AssetEmbedder>();
            window.maxSize = new Vector2(300f, 10000000);
            window.Focus();
        }

        protected override void OnGUI()
        {
            EditorGUILayout.BeginVertical("box");

            DrawProp("parentObject");
            DrawProp("assetsToEmbed");
            DrawProp("deleteOriginalAssets");

            EditorGUILayout.HelpBox("Embedding a copy of this asset will not update references from the original asset to the copy.", MessageType.Warning);

            if(IMGUIUtil.ConditionalButton(() => parentObject != null && assetsToEmbed.Count > 0, "Embed Assets"))
            {
                EmbedAssets();
            }

            EditorGUILayout.EndVertical();
            SerializedObject.ApplyModifiedProperties();
        }

        private void EmbedAssets()
        {
            AssetDatabase.StartAssetEditing();

            foreach(Object asset in assetsToEmbed)
            {
                try
                {
                    var copy = Instantiate(asset);
                    copy.name = copy.name.Replace("(Clone)", "");

                    if(deleteOriginalAssets)
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
    
                    AssetDatabase.AddObjectToAsset(copy, parentObject);
                }
                catch(Exception e)
                {
                    Debug.LogError($"Could not embed {asset} into {parentObject}\n{e}");
                }
                finally
                {
                }
            }

            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parentObject));
            AssetDatabase.StopAssetEditing();

            Close();
        }
    }
}
