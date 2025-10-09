using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Content.Characters.Base.Code
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseView : MonoBehaviour
    {
        protected CancellationToken _token;
        protected NavMeshAgent _agent;
        protected Animator _animator;
        protected bool _hasAgentPath = false;
        protected string ANIMATOR_PARAM_IS_WALKING = "IsWalk";
        
        public virtual void Init(CancellationToken cancellationToken)
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _token = cancellationToken;
        }

        public void SetAgentDestination(Vector3 targetPosition)
        {
            _agent.SetDestination(targetPosition);
        }

        public bool HasAgentPath()
        {
            if (_agent.hasPath)
                return true;

            return false;
        }

        public async UniTask MoveAgentTo(Vector3 targetPosition, Quaternion targetRotation)
        {
            SetAgentDestination(targetPosition);
            
            await UniTask.WaitUntil(() => HasAgentPath() == false, cancellationToken: _token);
            
            await gameObject.transform.DORotateQuaternion(targetRotation, 0.2f);
        }

        public void PlayAnimationIdle()
        {
            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, false);
        }

        public void PlayAnimationWalk()
        {
            _animator.SetBool(ANIMATOR_PARAM_IS_WALKING, true);
        }
    }
}