using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MrBones
{
    public class CinemachineMainCamera : MonoBehaviour
    {
        public new Camera camera;
        public CinemachineVirtualCamera virtualCamera;

        public void Awake()
        {
            MrBonesSpirit.OnMrBonesSpawned += SetFollowLookAtTarget;
        }

        private void SetFollowLookAtTarget(MrBonesSpirit obj)
        {
            var spirit = obj.Spirit;
            if (!spirit)
                return;

            var body = spirit.BodyObjectInstance;
            if(body)
            {
                virtualCamera.LookAt = body.transform;
                virtualCamera.Follow = body.transform;
            }
        }
    }
}
