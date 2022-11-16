using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Nebby;

namespace MrBones
{
    public class ScreamComponent : MonoBehaviour
    {
        [SerializeField] private FloatReference totalScreamEnergy;
        public float CurrentScreamEnergy
        {
            get
            {
                return _currentScreamEnergy.Value;
            }
            set
            {
                _currentScreamEnergy.Value = value;
                if (_currentScreamEnergy.Value < 0)
                    _currentScreamEnergy.Value = 0;
                if (_currentScreamEnergy.Value > totalScreamEnergy.Value)
                    _currentScreamEnergy.Value = totalScreamEnergy.Value;
            }
        }
        [SerializeField] private FloatReference _currentScreamEnergy;

        public void RestoreEnergyToFull()
        {
            _currentScreamEnergy.Value = totalScreamEnergy.Value;
        }
    }
}