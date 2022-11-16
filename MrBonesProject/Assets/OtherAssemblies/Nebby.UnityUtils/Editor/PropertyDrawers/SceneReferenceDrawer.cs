using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Linq;

namespace Nebby.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceDrawer : IMGUIPropertyDrawer
    {
        SerializedProperty sceneName;
        SerializedProperty scenePath;
        SerializedProperty sceneBuildIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            sceneName = property.FindPropertyRelative("_sceneName");
            scenePath = property.FindPropertyRelative("_scenePath");
            sceneBuildIndex = property.FindPropertyRelative("_sceneBuildIndex");
            label.tooltip = $"Scene Name: {sceneName.stringValue}\n" +
                $"Scene Path: {scenePath.stringValue}," +
                $"Scene Build Index: {sceneBuildIndex.intValue}";


            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath.stringValue);
            oldScene = oldScene != null ? oldScene : GetSceneAssetByName();
            var assetPath = AssetDatabase.GetAssetPath(oldScene);
            scenePath.stringValue = assetPath == scenePath.stringValue ? scenePath.stringValue : assetPath;
            if(!CheckIfSceneAssetIsInBuildSettings(oldScene))
            {
                ShowWindowAndDecideToAddSceneToBuildSettings(oldScene);
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            var newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset;
            
            if(EditorGUI.EndChangeCheck())
            {
                AssignNewScene(newScene);
            }
            EditorGUI.EndProperty();
        }


        private void AssignNewScene(SceneAsset sceneAsset)
        {
            if(sceneAsset == null)
            {
                scenePath.stringValue = string.Empty;
                sceneName.stringValue = string.Empty;
                sceneBuildIndex.intValue = -1;
                return;
            }

            var path = AssetDatabase.GetAssetPath(sceneAsset);
            if(!CheckIfSceneAssetIsInBuildSettings(sceneAsset)) //Not in build settings? prompt adding it.
            {
                if(ShowWindowAndDecideToAddSceneToBuildSettings(sceneAsset))
                {
                    AssignNewScene(sceneAsset);
                    scenePath.stringValue = path;
                    sceneName.stringValue = sceneAsset.name;
                    sceneBuildIndex.intValue = SceneUtility.GetBuildIndexByScenePath(path);
                }
            }
            scenePath.stringValue = path;
            sceneName.stringValue = sceneAsset.name;
            sceneBuildIndex.intValue = SceneUtility.GetBuildIndexByScenePath(path);
        }

        private SceneAsset GetSceneAssetByName()
        {
            var assets = AssetDatabase.FindAssets($"t:{nameof(SceneAsset)}")
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .Select(path => AssetDatabase.LoadAssetAtPath<SceneAsset>(path));

            return assets.Where(sceneAsset => sceneAsset.name == sceneName.stringValue).FirstOrDefault();
        }
        private bool CheckIfSceneAssetIsInBuildSettings(SceneAsset asset)
        {
            if (!asset)
                return true;

            var path = AssetDatabase.GetAssetPath(asset);
            var buildSettingsScenePaths = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            return buildSettingsScenePaths.Contains(path);
        }

        private bool ShowWindowAndDecideToAddSceneToBuildSettings(SceneAsset asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            var addToBuildSettings = EditorUtility.DisplayDialog("Scene is Invalid",
                    $"The Scene Asset {asset} is an invalid scene as it's not in your project's build settings.\n" +
                    $"Would you like to add it to your Build Settings?",
                    "Yes",
                    "No");

            if (!addToBuildSettings)
            {
                scenePath.stringValue = string.Empty;
                sceneName.stringValue = string.Empty;
                sceneBuildIndex.intValue = -1;
                return false;
            }
            var scenes = EditorBuildSettings.scenes;
            var newBuildScene = new EditorBuildSettingsScene(path, true);
            ArrayUtility.Add(ref scenes, newBuildScene);
            EditorBuildSettings.scenes = scenes;
            return true;
        }
    }
}