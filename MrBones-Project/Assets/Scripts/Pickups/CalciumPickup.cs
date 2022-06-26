﻿using Nebby.UnityUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones.Pickups
{
    [CreateAssetMenu(menuName = "MrBones/CalciumPickup")]
    public class CalciumPickup : PickupDef
    {
        public FloatReference calciumAmount;
    }
}
