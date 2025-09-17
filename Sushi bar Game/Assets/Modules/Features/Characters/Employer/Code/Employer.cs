using System;
using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using UnityEngine;

namespace Modules.Features
{
    public class Employer : BaseCharacterMover
    {
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;

        public override async UniTask MoveTo(Transform targetTransform, float timeToPosition)
        {
            await base.MoveTo(targetTransform, timeToPosition);

            await _loadingCircle.RunImmitation(_immitationTime);
        }
    }
}