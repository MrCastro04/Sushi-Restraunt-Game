using System;
using UnityEngine;

namespace Modules.Features.Characters.Employer.Code
{
    [RequireComponent(typeof(Animator))]
    public class EmployerServiceAnimator : MonoBehaviour
    {
        private Animator _animator;

        private string ANIMATOR_PARAM_IS_WALKING = "IsWalking";
        private string ANIMATOR_PARAM_IS_CHOP_FOOD = "IsChopFood";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAnimationIdle()
        {
            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, false);
        }

        public void PlayAnimationWalking(bool afterChopChopAnimation = false)
        {
            if (afterChopChopAnimation)
            {
                _animator.SetBool(ANIMATOR_PARAM_IS_CHOP_FOOD, false);
            }

            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, true);
        }

        public void PlayAnimationChopChopFood()
        {
            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, false);

            _animator.SetBool(ANIMATOR_PARAM_IS_CHOP_FOOD, true);
        }
    }
}