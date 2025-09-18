using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Features.Characters.Base.Code
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseCharacterMover : MonoBehaviour
    {
        protected NavMeshAgent _agent;

        protected virtual void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        protected async UniTask MoveTo(Vector3 targetPosition, Quaternion targetRotation)
        {
            _agent.SetDestination(targetPosition);

            await UniTask.WaitUntil(() => _agent.hasPath == false);

            await gameObject.transform.DORotateQuaternion(targetRotation, 0.2f);
        }
    }
}