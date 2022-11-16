using UnityEngine;

namespace Nebby
{
    public static class UnityExtensions
    {
        public static GameObject GetRootGameObject(this Component c) => GetRootGameObject(c.gameObject);
        public static GameObject GetRootGameObject(this Behaviour b) => GetRootGameObject(b.gameObject);
        public static GameObject GetRootGameObject(this MonoBehaviour mb) => GetRootGameObject(mb.gameObject);
        public static GameObject GetRootGameObject(this GameObject go) => go.transform.root.gameObject;

        public static T GetComponentFromRoot<T>(this Component c) => GetComponentFromRoot<T>(c.gameObject);
        public static T GetComponentFromRoot<T>(this Behaviour b) => GetComponentFromRoot<T>(b.gameObject);
        public static T GetComponentFromRoot<T>(this MonoBehaviour mb) => GetComponentFromRoot<T>(mb.gameObject);
        public static T GetComponentFromRoot<T>(this GameObject go) => GetRootGameObject(go).GetComponent<T>();

        public static void SetActiveSafe(this GameObject go, bool state)
        {
            go.GetComponent<IAnimationClipSource>();
            if (!go)
                return;

            go.SetActive(state);
        }

        public static bool CompareLayer(this Component c, int layerIndex) => CompareLayer(c.gameObject, layerIndex);
        public static bool CompareRootLayer(this Component c, int layerIndex) => CompareRootLayer(c.gameObject, layerIndex);
        public static bool CompareLayer(this Behaviour b, int layerIndex) => CompareLayer(b.gameObject, layerIndex);
        public static bool CompareRootLayer(this Behaviour b, int layerIndex) => CompareRootLayer(b.gameObject, layerIndex);
        public static bool CompareLayer(this MonoBehaviour mb, int layerIndex) => CompareLayer(mb.gameObject, layerIndex);
        public static bool CompareRootLayer(this MonoBehaviour mb, int layerIndex) => CompareLayer(mb.gameObject, layerIndex);
        public static bool CompareLayer(this GameObject go, int layerIndex) => go.layer == layerIndex;
        public static bool CompareRootLayer(this GameObject go, int layerIndex) => GetRootGameObject(go).layer == layerIndex;

        public static bool CompareTagFromRoot(this Component c, string tag) => CompareTagFromRoot(c.gameObject, tag);
        public static bool CompareTagFromRoot(this Behaviour b, string tag) => CompareTagFromRoot(b.gameObject, tag);
        public static bool CompareTagFromRoot(this MonoBehaviour mb, string tag) => CompareTagFromRoot(mb.gameObject, tag);
        public static bool CompareTagFromRoot(this GameObject go, string tag) => GetRootGameObject(go).CompareTag(tag);

        public static bool CompareTagObject(this Component c, TagObject tagObject, bool fromRoot = true) => CompareTagObject(c.gameObject, tagObject, fromRoot);
        public static bool CompareTagObject(this Behaviour b, TagObject tagObject, bool fromRoot = true) => CompareTagObject(b.gameObject, tagObject, fromRoot);
        public static bool CompareTagObject(this MonoBehaviour mb, TagObject tagObject, bool fromRoot = true) => CompareTagObject(mb.gameObject, tagObject, fromRoot);
        public static bool CompareTagObject(this GameObject go, TagObject tagObject, bool fromRoot = true)
        {
            var container = fromRoot ? go.GetComponentFromRoot<TagContainer>() : go.GetComponent<TagContainer>();
            return container && container.ObjectHasTag(tagObject);
        }

        public static bool CompareTagObjects(this Component c, bool fromRoot = true, params TagObject[] tagObjects) => CompareTagObjects(c.gameObject, fromRoot, tagObjects);
        public static bool CompareTagObjects(this Behaviour b, bool fromRoot = true, params TagObject[] tagObjects) => CompareTagObjects(b.gameObject, fromRoot, tagObjects);
        public static bool CompareTagObjects(this MonoBehaviour mb, bool fromRoot = true, params TagObject[] tagObjects) => CompareTagObjects(mb.gameObject, fromRoot, tagObjects);
        public static bool CompareTagObjects(this GameObject go, bool fromRoot = true, params TagObject[] tagObjects)
        {
            var container = fromRoot ? go.GetComponentFromRoot<TagContainer>() : go.GetComponent<TagContainer>();
            return container && container.ObjectHasTags(tagObjects);
        }

        public static void SetSettingsToSoundDef(this AudioSource source, SoundDef soundDef)
        {
            source.volume = soundDef.Volume;
            source.pitch = soundDef.Pitch;
            source.loop = soundDef.looping;
            source.clip = soundDef.clip;
        }
    }
}