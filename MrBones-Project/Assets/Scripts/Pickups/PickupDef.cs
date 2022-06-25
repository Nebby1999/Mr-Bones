using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;

namespace MrBones.Pickups
{
    [CreateAssetMenu(menuName = "MrBones/PickupDef")]
    public class PickupDef : ScriptableObject
    {
        public Sprite pickupSprite;
        public FloatReference calciumAmount;
        public float scale;
    }
}