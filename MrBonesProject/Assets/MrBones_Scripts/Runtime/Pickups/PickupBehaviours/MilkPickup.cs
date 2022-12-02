using System.Collections;
using UnityEngine;

namespace MrBones
{
    [RequireComponent(typeof(GenericPickupController))]
    public class MilkPickup : MonoBehaviour, IPickable
    {
        public void GrantPickupToPicker(PickupInfo pickupInfo)
        {
            StageController.Instance.OnMilkCollected();
        }

        public bool ShouldGrantPickup(PickupInfo pickupInfo)
        {
            return pickupInfo.pickerSpirit && pickupInfo.pickerSpirit.GetComponent<MrBonesSpirit>();
        }
    }
}