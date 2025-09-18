using System;
using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using Modules.Features.Characters.Customer;
using UnityEngine;
namespace Modules.Features.Characters.Employer.Code
{
    public class Employer : BaseCharacterMover
    {
        [SerializeField] private PointMono _gatheringPoint;
        [SerializeField] private PointMono _sellPoint;
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;

        public event Action OnSellFood;

        private void OnEnable()
        {
            EventsCustomer.OnCustomerGetBuyPoint += RunWorkFlow;
        }

        private void OnDisable()
        {
            EventsCustomer.OnCustomerGetBuyPoint -= RunWorkFlow;
        }

        private async void RunWorkFlow()
        {
            await GoToPoint(_sellPoint,true);

            await GoToPoint(_gatheringPoint, true);

            await GoToPoint(_sellPoint);
            
            OnSellFood?.Invoke();
        }
        
        private async UniTask GoToPoint(PointMono pointMono, bool withImmitation = false)
        {
            await MoveTo(pointMono.Position, pointMono.Rotation);

            if (withImmitation)
                await _loadingCircle.RunImmitation(_immitationTime);
        }
    }
}