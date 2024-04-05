using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class SpawnXAmountOfPrefab : MonoBehaviour
    {
        public GameObject prefab;
        public int amount;

        public void Spawn()
        {
            for(int i = 0; i < amount; i++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }
    }
}