using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Nebby;

namespace MrBones
{
    public class CinemachineMainCamera : SingletonBehaviour<CinemachineMainCamera>
    {
        public new Camera camera;
        public CinemachineVirtualCamera mainVirtualCamera;
        public CinemachineVirtualCamera auxiliaryVirtualCamera;
        public bool zoomIn;

        public void PanToObject(GameObject obj)
        {
            auxiliaryVirtualCamera.Follow = obj ? obj.transform : null;
            auxiliaryVirtualCamera.enabled = obj;
        }

        private void Awake()
        {
            MrBonesSpirit.OnMrBonesSpawned += SetFollowLookAtTarget;
        }

        private void OnDestroy()
        {
            MrBonesSpirit.OnMrBonesSpawned -= SetFollowLookAtTarget;
        }


        private void SetFollowLookAtTarget(MrBonesSpirit obj)
        {
            var spirit = obj.Spirit;
            if (!spirit)
                return;

            var body = spirit.BodyObjectInstance;
            if (body)
            {
                mainVirtualCamera.LookAt = body.transform;
                mainVirtualCamera.Follow = body.transform;
            }
        }
    }
}
