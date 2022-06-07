using UnityEngine;

namespace Nebby.UnityUtils
{
    public static class Extensions
    {
        public static GameObject GetRootGameObject(this GameObject go)
        {
            return go.transform.root.gameObject;
        }
    }
}