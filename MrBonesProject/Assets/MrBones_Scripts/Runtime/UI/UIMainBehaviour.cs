using Nebby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MrBones.UI
{
    [DisallowMultipleComponent]
    public class UIMainBehaviour : SingletonBehaviour<UIMainBehaviour>
    {
        public Canvas MainCanvas => _canvas;
        [SerializeField] private Canvas _canvas;

    }
}
