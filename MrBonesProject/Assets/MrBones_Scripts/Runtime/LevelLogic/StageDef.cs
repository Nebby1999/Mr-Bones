using Nebby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    [CreateAssetMenu(fileName = "New StageDef", menuName = "MrBones/StageDef")]
    public class StageDef : ScriptableObject
    {
        public SceneReference sceneToLoad;
        public int stageNumber;
        public string stageName;
        public int parTime;
        public StageDef nextStage;
    }
}
