using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    [RequireComponent(typeof(GenericPickupController))]
    public class GenericScorePickup : MonoBehaviour, IPickable
    {
        public Color pickupColor;
        public uint scoreAmount;

        public bool ShouldGrantPickup(PickupInfo pickupInfo)
        {
            if (!pickupInfo.pickerSpirit)
                return false;

            return pickupInfo.pickerSpirit.GetComponent<ScoreTracker>();
        }

        public void GrantPickupToPicker(PickupInfo pickupInfo)
        {
            var scoreTracker = pickupInfo.pickerSpirit.GetComponent<ScoreTracker>();
            scoreTracker.AddScore(scoreAmount);
            PickupParticleManager.Instance.DoBurst(pickupColor, transform.position);
        }
    }
}
