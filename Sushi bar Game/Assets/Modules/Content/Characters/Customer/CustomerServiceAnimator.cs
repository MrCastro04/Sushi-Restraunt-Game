using System;
using UnityEngine;

namespace Modules.Content.Characters.Customer
{
    [RequireComponent(typeof(Animator))]
    public class CustomerServiceAnimator : MonoBehaviour
    {
        private Animator _animator;
        private string ANIMATOR_PARAM_IS_WALKING = "IsWalking";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAnimationIdle()
        {
            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, false);
        }

        public void PlayAnimationWalking()
        {
            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, true);
        }
    }
}