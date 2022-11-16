using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.Editor.Inspectors;
using MrBones;
using UnityEditor;

namespace MrBones.Editor
{
    [CustomEditor(typeof(EntityStateMachine))]
    public class EntityStateMachineInspector : EntityStateMachineInspectorBase<EntityStateMachine>
    {
    }
}
