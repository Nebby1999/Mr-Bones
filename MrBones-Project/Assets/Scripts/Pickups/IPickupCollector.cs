using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;

namespace MrBones.Pickups
{
    public interface IPickupCollector
    {
        public abstract bool OnPickupInteraction(GameObject pickupObject, PickupDef pickupDef);
    }
}