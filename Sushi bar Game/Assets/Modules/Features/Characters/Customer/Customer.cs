using System;
using Cysharp.Threading.Tasks;
using Modules.Core.Services;
using Modules.Features.Characters.Base.Code;
using UnityEngine;
using Zenject;

namespace Modules.Features.Characters.Customer
{
    public class Customer : BaseEntity
    {
       [Inject] private ServiceMapPoint _serviceMapPoint;
       
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private bool _getFood = false;

        private PointType _desiredPointType => PointType.Buy;
        
        private void OnEnable()
        {
            EventsCustomer.OnGetFood += GetFood;
        }

        private void OnDisable()
        {
            EventsCustomer.OnGetFood -= GetFood;
        }

        public async void WorkFlow()
        {
            var buyPoint = _serviceMapPoint.RegisterAndGetAnyFreePointWithType(_desiredPointType);

            await MoveTo(buyPoint.Position, buyPoint.Rotation);

            EventsCustomer.ExecuteCustomerGetBuyPoint(buyPoint.ID, this);

            await UniTask.WaitUntil(() => _getFood);

            await MoveTo(_startPosition, _startRotation);
            
            _getFood = false;
            
            EventsCustomer.ExecuteCustomerLeft(this);
        }

        private void GetFood(string pointID,Customer customer)
        {
            if (customer != this) return;

            _getFood = true;
        }
    }
}