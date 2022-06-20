using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrBones
{
    public class MrBonesAnimatorController : MonoBehaviour
    {
        public Animator animator;

        public float ScreamParam { get; set; }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("Scream", ScreamParam);
        }
    }
}
