using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;

namespace MrBones.Pickups
{
    public abstract class PickupDef : ScriptableObject
    {
        public Sprite pickupSprite;
        public float scale;
    }
}