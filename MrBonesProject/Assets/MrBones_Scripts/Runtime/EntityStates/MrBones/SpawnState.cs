using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityStates.MrBones
{
    public class SpawnState : MrBonesBaseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            ScreamComponent.RestoreEnergyToFull();
            outer.SetNextStateToMain();
        }
    }
}
