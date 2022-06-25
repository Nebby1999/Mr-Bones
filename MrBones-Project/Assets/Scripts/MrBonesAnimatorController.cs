using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    public class MrBonesAnimatorController : MonoBehaviour
    {
        public Animator animator;

        public float ScreamParam { get; set; }
        public bool IsCharging { get; set; }
        public bool Dead { get; set; }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("Scream", ScreamParam);
            animator.SetBool("IsCharging", IsCharging);
            animator.SetBool("Dead", Dead);
        }
    }
}
