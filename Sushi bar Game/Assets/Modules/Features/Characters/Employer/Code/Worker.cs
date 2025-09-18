using Modules.Features.Characters.Base.Code;
using Modules.Features.Characters.Client;
using UnityEngine;

namespace Modules.Features.Characters.Employer.Code
{
    public class Worker : BaseCharacterMover
    {
        [SerializeField] private Transform _sellPoint;
        [SerializeField] private Customer _customer;  
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;
        [SerializeField] private float _timeToPoint;

        private void OnEnable()
        {
            _customer.OnGetDestination += MoveTo;
        }

        private void OnDisable()
        {
            _customer.OnGetDestination -= MoveTo;
        }

        private async void MoveTo()
        {
            await base.MoveTo(_sellPoint, _timeToPoint);

            await _loadingCircle.RunImmitation(_immitationTime);
        }
    }
}