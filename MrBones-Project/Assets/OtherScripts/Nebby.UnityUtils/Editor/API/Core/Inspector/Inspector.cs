using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UObject = UnityEngine.Object;

namespace Nebby.UnityUtils.Editor.Core.Inspectors
{
    using static Nebby.UnityUtils.Editor.Core.TemplateHelpers;

    public struct ContextMenuData
    {
        public string menuName;

        public Action<DropdownMenuAction> menuAction;

        public Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCheck;


        public ContextMenuData(string name, Action<DropdownMenuAction> action)
        {
            menuName = name;
            menuAction = action;
            actionStatusCheck = x => DropdownMenuAction.Status.Normal;
        }

        public ContextMenuData(string name, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> statusCheck)
        {
            menuName = name;
            menuAction = action;
            actionStatusCheck = statusCheck;
        }
    }

    public abstract class Inspector<TObject> : UnityEditor.Editor where TObject : UObject
    {
        protected VisualElement RootVisualElement
        {
            get
            {
                if(_rootVisualElement == null)
                {
                    _rootVisualElement = new VisualElement();
                    _rootVisualElement.name = "Inspector_RootElement";
                }
                return _rootVisualElement;
            }
        }
        private VisualElement _rootVisualElement;

        protected TObject TargetType { get => target as TObject; }

        protected virtual bool HasVisualTreeAsset { get; } = true;

        private Dictionary<VisualElement, (ContextualMenuManipulator, List<ContextMenuData>)> elementToContextMenu = new Dictionary<VisualElement, (ContextualMenuManipulator, List<ContextMenuData>)>();

        protected virtual void OnEnable()
        {
            try
            {
                GetTemplateInstance(GetType().Name, RootVisualElement, ValidateUXMLPath);
            }
            catch(Exception e)
            {
                if (HasVisualTreeAsset)
                    Debug.LogError(e);
            }
        }
        protected virtual void OnDisable() { }

        protected virtual bool ValidateUXMLPath(string path)
        {
            return path.Contains("Nebby.UnityUtils", StringComparison.OrdinalIgnoreCase);
        }

        public sealed override VisualElement CreateInspectorGUI()
        {
            FinalizeInspectorGUI();
            return RootVisualElement;
        }

        protected abstract void FinalizeInspectorGUI();

        protected IMGUIContainer CreateHelpBox(string message, MessageType messageType)
        {
            IMGUIContainer container = new IMGUIContainer();
            container.name = $"Inspector_HelpBox";
            container.onGUIHandler = () =>
            {
                EditorGUILayout.HelpBox(message, messageType);
            };

            return container;
        }

        protected void AddSimpleContextMenu(VisualElement element, ContextMenuData contextMenuData)
        {
            if (!elementToContextMenu.ContainsKey(element))
            {
                var manipulator = new ContextualMenuManipulator(x => CreateMenu(element, x));
                elementToContextMenu.Add(element, (manipulator, new List<ContextMenuData>()));
                element.AddManipulator(manipulator);
            }
            var tuple = elementToContextMenu[element];
            if (!tuple.Item2.Contains(contextMenuData))
                tuple.Item2.Add(contextMenuData);
        }

        private void CreateMenu(VisualElement element, ContextualMenuPopulateEvent populateEvent)
        {
            var contextMenus = elementToContextMenu[element].Item2;

            foreach (ContextMenuData data in contextMenus)
            {
                populateEvent.menu.AppendAction(data.menuName, data.menuAction, data.actionStatusCheck);
            }
        }
    }
}
