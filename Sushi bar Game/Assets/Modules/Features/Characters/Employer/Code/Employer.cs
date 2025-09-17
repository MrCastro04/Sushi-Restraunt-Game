using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using UnityEngine;

namespace Modules.Features
{
    public class Employer : BaseCharacterMover
    {
        [SerializeField] private float _immitationTime;
        [SerializeField] private LoadingCircle _loadingCircle;

        public override async UniTask MoveTo(Transform targetTransform, float timeToPosition)
        {
            await base.MoveTo(targetTransform, timeToPosition);

            _loadingCircle.RunImmitation(_immitationTime);
        }
    }
}