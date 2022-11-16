using System.Collections;
using UnityEngine;

namespace MrBones.Pickups
{
    [RequireComponent(typeof(GenericPickupController))]
    public class MilkPickup : MonoBehaviour, IPickable
    {
        public UnityEngine.Events.UnityEvent onPickedUp;
        public void GrantPickupToPicker(PickupInfo pickupInfo)
        {
            onPickedUp?.Invoke();
        }

        public bool ShouldGrantPickup(PickupInfo pickupInfo)
        {
            return pickupInfo.pickerSpirit && pickupInfo.pickerSpirit.GetComponent<MrBonesSpirit>();
        }
    }
}