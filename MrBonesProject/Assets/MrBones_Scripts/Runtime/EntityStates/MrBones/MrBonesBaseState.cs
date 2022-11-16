using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrBones;
using Nebby;

namespace EntityStates.MrBones
{
    public abstract class MrBonesBaseState : EntityState
    {
        public ScreamComponent ScreamComponent => _screamComponent;
        private ScreamComponent _screamComponent;
        public MrBonesMovement MrBonesMovement => _mrBonesMovement;
        private MrBonesMovement _mrBonesMovement;

        public bool CanScream => ScreamComponent.CurrentScreamEnergy > 0;
        public float ScreamEnergy => ScreamComponent.CurrentScreamEnergy;

        public override void OnEnter()
        {
            base.OnEnter();
            _screamComponent = GetComponent<ScreamComponent>();
            _mrBonesMovement = GetComponent<MrBonesMovement>();
        }
    }
}
