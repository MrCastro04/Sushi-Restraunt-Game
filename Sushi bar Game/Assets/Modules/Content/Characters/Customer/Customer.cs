using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Content.Characters.Customer
{
    [RequireComponent(typeof(CustomerServiceAnimator))]
    public class Customer : BaseEntity
    {
        [Inject] private ServiceMapPoint _serviceMapPoint;

        private CustomerServiceAnimator _customerServiceAnimator;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private FoodType _desiredFoodType = FoodType.Sushi;
        private bool _hasFood = false;

        public FoodType DesiredFoodType => _desiredFoodType;
        private PointType _desiredPointType => PointType.Buy;

        #region Initialize

        protected override void Awake()
        {
            base.Awake();

            _customerServiceAnimator = GetComponent<CustomerServiceAnimator>();

            _startPosition = transform.position;

            _startRotation = transform.rotation;
        }

        #endregion

        #region EventsSubscription

        private void OnEnable()
        {
            EventsCustomer.OnGetFood += GetFood;
        }

        private void OnDisable()
        {
            EventsCustomer.OnGetFood -= GetFood;
        }
        
        #endregion

        
        public async void WorkFlow()
        {
            var buyPoint = _serviceMapPoint.GetAnyFreePointWithType(_desiredPointType);

            _serviceMapPoint.SetNonEmptyPointWithID(buyPoint.ID);
            
            await GoToPoint(buyPoint.Position, buyPoint.Rotation);

            EventsCustomer.ExecuteCustomerEnterBuyPoint(buyPoint.ID, this);

            await UniTask.WaitUntil(() => _hasFood);

            _serviceMapPoint.SetEmptyPointWithID(buyPoint.ID);
            
            await GoToPoint(_startPosition, _startRotation);

            _hasFood = false;

            EventsCustomer.ExecuteCustomerLeft(this);
        }
        
        private async UniTask GoToPoint(Vector3 pointPosiiton, Quaternion pointRotation)
        { 
            _customerServiceAnimator.PlayAnimationWalking();
            
            await MoveTo(pointPosiiton, pointRotation);
            
            _customerServiceAnimator.PlayAnimationIdle();
        }

        private void GetFood(string pointID, Customer customer)
        {
            if (customer != this) return;

            _hasFood = true;
        }
    }
}