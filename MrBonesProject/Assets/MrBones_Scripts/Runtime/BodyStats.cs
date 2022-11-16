using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    [CreateAssetMenu(fileName = "New BodyStats", menuName = "MrBones/BodyStats")]
    public class BodyStats : ScriptableObject
    {
        public float health;
        public float speed;
        public int jumpCount;
    }
}
