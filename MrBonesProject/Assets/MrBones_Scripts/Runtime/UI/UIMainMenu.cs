using Nebby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones.UI
{
    public class UIMainMenu : MonoBehaviour
    {
        public ChildLocator childLocator;

        public void ChangeMenu(string childLocatorEntry)
        {
            Transform childTransform = childLocator.FindChild(childLocatorEntry);
            if(childTransform)
            {
                foreach(ChildLocator.NameTransformPair pair in childLocator.Entries)
                {
                    pair.tiedTransform.gameObject.SetActive(false);
                }
                childTransform.gameObject.SetActive(true);
            }
        }

        public void OnQuitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
