using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Modules.Features.Characters.Base.Code
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEntity : MonoBehaviour
    {
       [SerializeField] protected NavMeshAgent _agent;
        protected CancellationToken _cancellationToken => this.GetCancellationTokenOnDestroy();

        protected async UniTask MoveTo(Vector3 targetPosition, Quaternion targetRotation)
        {
            _agent.SetDestination(targetPosition);

            await UniTask.WaitUntil(() => _agent.hasPath == false, cancellationToken:_cancellationToken);

            await gameObject.transform.DORotateQuaternion(targetRotation, 0.2f);
        }
    }
}