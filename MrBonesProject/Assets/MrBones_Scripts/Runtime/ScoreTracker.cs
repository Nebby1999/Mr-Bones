using Nebby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class ScoreTracker : MonoBehaviour
    {
        public uint Score => _score.Value;
        [SerializeField] private UIntReference _score;

        private void Awake()
        {
            _score.Value = 0u;
        }
        public void AddScore(uint amount)
        {
            _score.Value += amount;
        }

        public void DeductScore(uint amount)
        {
            _score.Value -= amount;
        }
    }
}
