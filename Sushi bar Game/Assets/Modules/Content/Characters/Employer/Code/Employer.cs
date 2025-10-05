using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using Modules.Content.Characters.Customer;
using Modules.Content.Map_Points;
using Modules.Content.UI.Circle_Loading.Code;
using Modules.Core.Serializeable_Collections.Map_Points;
using Modules.Core.Services;
using Modules.New;
using UnityEngine;
using Zenject;


namespace Modules.Content.Characters.Employer.Code
{
    [RequireComponent(typeof(EmployerServiceAnimator))]
    public class Employer : BaseEntity
    {
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;

        private ServiceFoodGenerators _serviceFoodGenerators;
        private ServiceMapPoint _serviceMapPoint;
        private ServiceCustomerQueue _serviceCustomerQueue;
        private EmployerServiceAnimator _employerServiceAnimator;
        private PointMono _gatheringPoint;
        private bool _isBusy = false;

        #region Initialize

        [Inject]
        private void Construct(
            ServiceFoodGenerators serviceFoodGenerators,
            ServiceMapPoint serviceMapPoint,
            ServiceCustomerQueue serviceCustomerQueue,
            CollectionPointsMono collectionPointsMono)
        {
            _serviceFoodGenerators = serviceFoodGenerators;

            _serviceMapPoint = serviceMapPoint;

            _serviceCustomerQueue = serviceCustomerQueue;

            _gatheringPoint = _serviceMapPoint.GetAnyFreePointWithType(PointType.GatheringFood);
        }

        protected override void Awake()
        {
            base.Awake();

            _employerServiceAnimator = GetComponent<EmployerServiceAnimator>();
        }

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            EventsCustomer.OnEnterBuyPoint += RunWorkFlow;
        }

        private void OnDisable()
        {
            EventsCustomer.OnEnterBuyPoint -= RunWorkFlow;
        }

        #endregion

        #region WorkFlow

        private async void RunWorkFlow(string pointID, Content.Characters.Customer.Customer customer)
        {
            if (_serviceCustomerQueue.IsContainsCustomerID(pointID) &
                _serviceCustomerQueue.IsContainsCustomer(customer))
            {
                Debug.Log("Такой клиент уже есть в очереди!");
            }
            else
            {
                _serviceCustomerQueue.AddNewCustomer(pointID, customer);
            }

            if (_isBusy)
            {
                Debug.Log("Работник занят. Не может обслуживать");
                return;
            }

            _isBusy = true;

            await WorkFlow(pointID, customer);

            if (_serviceCustomerQueue.IsQueueEmpty() == false)
            {
                Debug.Log("очередь не пустая");
                _isBusy = false;
                RunWorkFlow(_serviceCustomerQueue.GetPeekCustomerID(), _serviceCustomerQueue.GetPeekCustomer());
            }
            else
            {
                Debug.Log("очередь пустая");
                _isBusy = false;
                // засыпаем
            }
        }

        private async UniTask WorkFlow(string pointID, Customer.Customer customer)
        {
            var sellPoint = _serviceMapPoint.GetNeighboringPointForEmployer(pointID);

            _serviceMapPoint.SetNonEmptyPointWithID(sellPoint.ID);

            await GoToPoint(sellPoint, true);

            var generator = _serviceFoodGenerators.GetFreeFoodGeneratorByFoodType(customer.DesiredFoodType);

            await GoToPoint(generator.PointMono);

            await generator.StartUse();

            await GoToPoint(sellPoint, false, true);

            EventsCustomer.ExecuteCustomerGetFood(pointID, customer);

            _serviceMapPoint.SetEmptyPointWithID(sellPoint.ID);

            _serviceCustomerQueue.RemoveCurrentCustomer();
        }

        #endregion

        private async UniTask GoToPoint(PointMono pointMono, bool withImmitation = false, bool withFood = false)
        {
            _employerServiceAnimator.PlayAnimationWalking(withFood);

            await MoveTo(pointMono.Position, pointMono.Rotation);

            switch (pointMono.PointType)
            {
                case PointType.Sell:
                    _employerServiceAnimator.PlayAnimationIdle();
                    break;

                case PointType.GatheringFood:
                    _employerServiceAnimator.PlayAnimationChopChopFood();
                    break;
            }

            if (withImmitation)
            {
                await _loadingCircle.RunImmitation(_immitationTime);
            }
        }
    }
}