using System;
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

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        protected async UniTask MoveTo(Transform targetTransform, float timeToPosition)
        {
            _agent.SetDestination(targetTransform.position);
            
            await UniTask.WaitWhile(() => _agent.hasPath).AsTask().AsUniTask();
            
            await gameObject.transform
                .DORotateQuaternion(targetTransform.rotation, 0.2f)
                .AsyncWaitForCompletion();  
        }
    }
}