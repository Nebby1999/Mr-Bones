using Nebby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class GameEventManager : SingletonBehaviour<GameEventManager>
    {
        public static event Action<PickupInfo> OnPickupPickedUp;

        private static GameObject pickupBurstPrefab;

        [RuntimeInitializeOnLoadMethod]
        private static void RuntimeInit()
        {
            pickupBurstPrefab = Resources.Load<GameObject>("PickupBurst");
        }

        public void OnPickup(PickupInfo pickupInfo)
        {
            GenericPickupController controller = pickupInfo.controller;
            if (!controller)
                return;

            var instance = Instantiate(pickupBurstPrefab, controller.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var main = instance.main;
            main.startColor = new ParticleSystem.MinMaxGradient(controller.pickupColor);
            instance.Play();

            if(controller.pickUpSound)
            {
                controller.pickUpSound.Play();
            }

            OnPickupPickedUp?.Invoke(pickupInfo);
        }
    }
}