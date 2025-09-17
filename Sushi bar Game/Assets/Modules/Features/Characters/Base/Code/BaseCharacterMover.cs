using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Modules.Features.Characters.Base.Code
{
    public abstract class BaseCharacterMover : MonoBehaviour
    {
        public virtual async UniTask MoveTo(Transform targetTransform, float timeToPosition)
        {
          UniTask moveTask = gameObject.transform.DOMove(targetTransform.position, timeToPosition).AsyncWaitForCompletion().AsUniTask();

          UniTask rotateTask = gameObject.transform.DOLookAt(targetTransform.position, timeToPosition).AsyncWaitForCompletion().AsUniTask();

          await UniTask.WhenAll(moveTask,rotateTask);

          await gameObject.transform.DORotateQuaternion(targetTransform.rotation, 0.2f).AsyncWaitForCompletion();
        }
    }
}