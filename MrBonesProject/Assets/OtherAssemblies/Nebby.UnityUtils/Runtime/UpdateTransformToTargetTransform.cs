using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    public sealed class UpdateTransformToTargetTransform : MonoBehaviour
    {
        public Transform target;

        public bool updatePos;
        public bool updateRot;
        public bool updateScale;

        public void Update()
        {
            if (updatePos)
                UpdatePos();
            if (updateRot)
                UpdateRot();
            if (updateScale)
                UpdateScale();
        }

        private void UpdatePos() => transform.position = target.position;
        private void UpdateRot() => transform.rotation = target.rotation;
        private void UpdateScale() => transform.localScale = target.localScale;
    }
}
