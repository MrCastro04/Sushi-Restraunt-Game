using System;
using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using Modules.Features.Characters.Client;
using UnityEngine;

namespace Modules.Features.Characters.Worker.Code
{
    public class Worker : BaseCharacterMover
    {
        [SerializeField] private PointMono _gatheringPoint;
        [SerializeField] private PointMono _sellPoint;
        [SerializeField] private Customer _customer;
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;

        public event Action OnSellFood;

        private void OnEnable()
        {
            _customer.OnGetDestination += RunWorkFlow;
        }

        private void OnDisable()
        {
            _customer.OnGetDestination -= RunWorkFlow;
        }

        private async void RunWorkFlow()
        {
            await MoveTo(_sellPoint.Position, _sellPoint.Rotation);

            await _loadingCircle.RunImmitation(_immitationTime);

            await MoveTo(_gatheringPoint.Position, _gatheringPoint.Rotation);

            await _loadingCircle.RunImmitation(_immitationTime);

            await MoveTo(_sellPoint.Position, _sellPoint.Rotation);

            OnSellFood?.Invoke();
        }
    }
}