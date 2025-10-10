using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using Modules.Content.Characters.Customer.Controller;
using Modules.Content.Characters.Customer.Events;
using Modules.Content.Characters.Employer.Model;
using Modules.Content.Characters.Employer.View;
using Modules.Content.Food_Generator;
using Modules.Content.FoodCollection;
using Modules.Core.Serializeable_Collections.Map_Points;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Content.Characters.Employer.Controller
{
    [RequireComponent(typeof(ViewEmployer))]
    public class ControllerEmployer : BaseController
    {
        private ModelEmployer _modelEmployer;
        private ViewEmployer _viewEmployer;

        private ServiceFoodGenerators _serviceFoodGenerators;
        private ServiceMapPoint _serviceMapPoint;
        private ServiceCustomerQueue _serviceCustomerQueue;

        #region INITIALIZE

        [Inject]
        private void Construct(
            ModelEmployer modelEmployer,
            ServiceFoodGenerators serviceFoodGenerators,
            ServiceMapPoint serviceMapPoint,
            ServiceCustomerQueue serviceCustomerQueue,
            CollectionPointsMono collectionPointsMono)
        {
            _modelEmployer = modelEmployer;
            
            _serviceFoodGenerators = serviceFoodGenerators;

            _serviceMapPoint = serviceMapPoint;

            _serviceCustomerQueue = serviceCustomerQueue;
            
            Init();
        }

        public override void Init()
        {
            base.Init();

            _viewEmployer = GetComponent<ViewEmployer>();
            
            _viewEmployer.SetImmitationTime(_modelEmployer.ImmitationTime);
        }

        #endregion
        
        #region EVENT_SUBSCRIPTION

        private void OnEnable()
        {
            EventsCustomer.OnEnterBuyPoint += TryWorkFlow;
            EventsEmployer.OnEmployerStartCook += HandlerEmployerStartCook;
        }

        private void OnDisable()
        {
            EventsCustomer.OnEnterBuyPoint -= TryWorkFlow;
            EventsEmployer.OnEmployerStartCook -= HandlerEmployerStartCook;
        }

        #endregion

        private void HandlerEmployerStartCook(ControllerEmployer controllerEmployer, FoodType foodType)
        {
            if(controllerEmployer != this) return;
            
        _viewEmployer.PlayAnimationCook(foodType);
        }

        #region WORK_FLOW

        private async void TryWorkFlow(string pointID, ControllerCustomer controllerCustomer, FoodType foodType)
        {
            if (_serviceCustomerQueue.IsContainsCustomerID(pointID) &
                _serviceCustomerQueue.IsContainsCustomer(controllerCustomer))
            {
                Debug.Log("Такой клиент уже есть в очереди!");
            }
            else
            {
                _serviceCustomerQueue.AddNewCustomer(pointID, controllerCustomer);
            }

            if (_modelEmployer.IsBusy)
            {
                Debug.Log("Работник занят. Не может обслуживать");
                return;
            }
            
            await WorkFlow(pointID, controllerCustomer,foodType);

            if (_serviceCustomerQueue.IsQueueEmpty() == false)
            {
                Debug.Log("очередь не пустая");
                
                TryWorkFlow(_serviceCustomerQueue.GetPeekCustomerID(), _serviceCustomerQueue.GetPeekCustomer(), foodType);
            }
            else
            {
                Debug.Log("очередь пустая");
                // засыпаем
            }
        }

        private async UniTask WorkFlow(string pointID, ControllerCustomer controllerCustomer, FoodType foodType)
        {
            _modelEmployer.SetIsBusyStatus(true);
            
            var sellPoint = _serviceMapPoint.GetNeighboringPointForEmployer(pointID);

            _serviceMapPoint.SetNonEmptyPointWithID(sellPoint.ID);
            
            await _viewEmployer.GoToPoint(sellPoint, true);

            var generator = _serviceFoodGenerators.GetFreeFoodGeneratorByFoodType(foodType);

            await _viewEmployer.GoToPoint(generator.PointMono);
            
            await generator.StartUse(this);

            await _viewEmployer.GoToPoint(sellPoint, false, true);

            EventsCustomer.ExecuteCustomerGetFood(pointID, controllerCustomer);

            _serviceMapPoint.SetEmptyPointWithID(sellPoint.ID);

            _serviceCustomerQueue.RemoveCurrentCustomer();
            
            _modelEmployer.SetIsBusyStatus(false);
        }
        #endregion
    }
}