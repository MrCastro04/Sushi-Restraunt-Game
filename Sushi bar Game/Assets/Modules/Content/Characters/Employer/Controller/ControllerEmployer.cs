using System;
using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using Modules.Content.Characters.Customer.Controller;
using Modules.Content.Characters.Customer.Events;
using Modules.Content.Characters.Employer.Events;
using Modules.Content.Characters.Employer.Model;
using Modules.Content.Characters.Employer.View;
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

        public bool IsBusy => _modelEmployer.IsBusy;

        [Inject]
        private void Construct(
            ModelEmployer modelEmployer,
            ServiceFoodGenerators serviceFoodGenerators,
            ServiceMapPoint serviceMapPoint,
            CollectionPointsMono collectionPointsMono)
        {
            _modelEmployer = modelEmployer;
            _serviceFoodGenerators = serviceFoodGenerators;
            _serviceMapPoint = serviceMapPoint;

            Init();
        }

        public override void Init()
        {
            base.Init();

            _viewEmployer = GetComponent<ViewEmployer>();

            _viewEmployer.SetImmitationTime(_modelEmployer.ImmitationTime);
        }

        private void OnEnable()
        {
            EventsEmployer.OnEmployerStartCook += HandlerEmployerStartCook;
        }

        private void OnDisable()
        {
            EventsEmployer.OnEmployerStartCook -= HandlerEmployerStartCook;
        }

        private void HandlerEmployerStartCook(ControllerEmployer controllerEmployer, FoodType foodType)
        {
            if (controllerEmployer != this) return;

            _viewEmployer.PlayAnimationCook(foodType);
        }

        public void StartWorkflow(string pointID, ControllerCustomer controllerCustomer, FoodType foodType,
            Action onComplete)
        {
            if (_modelEmployer.IsBusy)
            {
                onComplete?.Invoke();
                return;
            }

            ExecuteWorkflow(pointID, controllerCustomer, foodType, onComplete).Forget();
        }

        private async UniTaskVoid ExecuteWorkflow(string pointID, ControllerCustomer controllerCustomer, FoodType foodType, Action onComplete)
        {
            await WorkFlow(pointID, controllerCustomer, foodType);

            onComplete?.Invoke();
        }

        private async UniTask WorkFlow(string pointID, ControllerCustomer controllerCustomer, FoodType foodType)
        {
            _modelEmployer.SetIsBusyStatus(true);

            var sellPoint = _serviceMapPoint.GetNeighboringPointForEmployer(pointID);

            if (sellPoint == null)
            {
                _modelEmployer.SetIsBusyStatus(false);
                return;
            }

            await _viewEmployer.GoToPoint(sellPoint, true);

            var generator = _serviceFoodGenerators.GetFreeFoodGeneratorByFoodType(foodType);

            if (generator == null)
            {
                _serviceMapPoint.SetEmptyPointWithID(sellPoint.ID);
                _modelEmployer.SetIsBusyStatus(false);
                return;
            }

            await _viewEmployer.GoToPoint(generator.PointMono);

            await generator.StartUse(this);

            var viewFood = generator.GetViewFood(this, _viewEmployer.ViewFoodTransform.position);

            await _viewEmployer.GoToPoint(sellPoint, false, true);

            viewFood.Hide();

            EventsEmployer.ExecuteEventOnEmployerSellFood(viewFood, generator.CurrentProfit);

            EventsCustomer.ExecuteCustomerGetFood(pointID, controllerCustomer);

            _serviceMapPoint.SetEmptyPointWithID(sellPoint.ID);

            _modelEmployer.SetIsBusyStatus(false);
        }
    }
}