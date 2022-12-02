using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    [RequireComponent(typeof(GenericPickupController))]
    public class GenericScreamPickup : MonoBehaviour, IPickable
    {
        public Color pickupColor;
        public float screamPowerRestored;
        public bool ShouldGrantPickup(PickupInfo pickupInfo)
        {
            if(!pickupInfo.pickerObject)
            {
                return false;
            }
            return pickupInfo.pickerObject.GetComponent<ScreamComponent>();
        }
        public void GrantPickupToPicker(PickupInfo pickupInfo)
        {
            var screamComponent = pickupInfo.pickerObject.GetComponent<ScreamComponent>();
            screamComponent.CurrentScreamEnergy += screamPowerRestored;
            PickupParticleManager.Instance.DoBurst(pickupColor, transform.position);
        }
    }
}