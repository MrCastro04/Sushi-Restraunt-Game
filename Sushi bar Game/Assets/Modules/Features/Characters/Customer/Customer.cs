using System;
using Cysharp.Threading.Tasks;
using Modules.Core;
using Modules.Features.Characters.Base.Code;
using UnityEditor.PackageManager;
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

        protected override void Awake()
        {
            base.Awake();

            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            EventsCustomer.OnGetFood += GetFood;
        }

        private void OnDisable()
        {
            EventsCustomer.OnGetFood -= GetFood;
        }

        private async void Start()
        {
            var buyPoint = _serviceMapPoint.GetFreePointByType(_desiredPointType);

            await MoveTo(buyPoint.Position, buyPoint.Rotation);

            EventsCustomer.ExecuteCustomerGetBuyPoint(buyPoint.ID,this);

            await UniTask.WaitUntil(() => _getFood);

            await MoveTo(_startPosition, _startRotation);
        }

        private void GetFood(Customer customer)
        {
            if(customer != this) return;

            _getFood = true;
        }
    }
}