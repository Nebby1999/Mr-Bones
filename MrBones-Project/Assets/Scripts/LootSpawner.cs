using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nebby.UnityUtils;
using Nebby.CSharpUtils;
using MrBones.Pickups;
using UnityEngine;
using System;

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
            foreach(PickupLoot loot in Loot)
            {
                if(ShouldSpawn(loot.chance))
                {
                    for(int i = 0; i < loot.amount; i++)
                    {
                        var go = Instantiate(pickupPrefab, transform.position, transform.rotation);
                        GenericPickupController controller = go.GetComponent<GenericPickupController>();
                        controller.pickupDef = loot.PickupDef;
                    }
                }
            }
        }

        public bool ShouldSpawn(float chance)
        {
            if (chance >= 1)
                return true;
            else if (chance <= 0)
                return false;

            float num = UnityEngine.Random.Range(0, 1);
            return chance > num;
        }
    }
}
