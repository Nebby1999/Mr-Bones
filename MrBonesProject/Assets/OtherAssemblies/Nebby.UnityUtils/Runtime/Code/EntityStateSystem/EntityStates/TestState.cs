using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EntityStates
{
    public class TestState : EntityStateBase
    {
        public static float testFloat;
        [SerializeField]
        public Nebby.SoundDef beginningSound;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("OnEnter");
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("Update");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Debug.Log("FixedUpdate");
            if(FixedAge > 10)
            {
                outer.SetNextState(new Uninitialized());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("OnExit");
        }
    }
}
