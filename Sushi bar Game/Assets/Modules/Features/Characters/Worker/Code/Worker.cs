using Modules.Features.Characters.Base.Code;
using Modules.Features.Characters.Client;
using UnityEngine;

namespace Modules.Features.Characters.Worker.Code
{
    public class Worker : BaseCharacterMover
    {
        [SerializeField] private Transform _gatheringPoint;
        [SerializeField] private Transform _sellPoint;
        [SerializeField] private Customer _customer;  
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;

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
            await MoveTo(_sellPoint);

            await _loadingCircle.RunImmitation(_immitationTime);

            await MoveTo(_gatheringPoint);

            await _loadingCircle.RunImmitation(_immitationTime);

            await MoveTo(_sellPoint);
        }
    }
}