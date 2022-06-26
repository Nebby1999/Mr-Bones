using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;
using MrBones.Pickups;
using UnityEngine;

namespace MrBones
{
    public class LootSpawner : MonoBehaviour
    {
        [Serializable]
        public struct PickupLoot
        {
            public PickupDef PickupDef;
            public int amount;
            [Range(0, 1)]
            public float chance;
        }
        public GameObject pickupPrefab;
        public PickupLoot[] Loot;
        public void Spawn()
        {

        }
    }
}
