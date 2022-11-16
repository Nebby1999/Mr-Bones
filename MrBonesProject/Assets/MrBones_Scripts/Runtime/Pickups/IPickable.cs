﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones.Pickups
{
    public struct PickupInfo
    {
        public IPickable pickableObject;
        public GameObject pickerObject;
        public CharacterBody pickerBody;
        public CharacterSpirit pickerSpirit;
    }
    public interface IPickable
    {
        public abstract bool ShouldGrantPickup(PickupInfo pickupInfo);

        public abstract void GrantPickupToPicker(PickupInfo pickupInfo);
    }
}