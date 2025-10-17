using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Base.Code;
using Modules.Content.Characters.Customer.Code.Events;
using Modules.Content.Characters.Customer.Code.Model;
using Modules.Content.Characters.Customer.Code.View;
using Modules.Content.FoodCollection;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Content.Characters.Customer.Code.Controller
{
    [RequireComponent(typeof(ViewCustomer))]
    public class ControllerCustomer : BaseController
    {
        private ModelCustomer _modelCustomer;
        private ViewCustomer _viewCustomer;
        private ServiceMapPoint _serviceMapPoint;

        public FoodType DesiredFoodType => DesiredFoodType;
        
        #region Initialize

        [Inject]
        private void Construct(ModelCustomer modelCustomer, ServiceMapPoint serviceMapPoint)
        {
            _modelCustomer = modelCustomer;
            _serviceMapPoint = serviceMapPoint;
            
            Init();
        }
        
        public override void Init()
        {
            base.Init();
            
            _viewCustomer = GetComponent<ViewCustomer>();
            
            _modelCustomer.SetPositionAndRotation(transform.position, transform.rotation);
        }

        #endregion
        
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
            var buyPoint = _serviceMapPoint.GetAnyFreePointWithType(_modelCustomer.PointType);

            _serviceMapPoint.SetNonEmptyPointWithID(buyPoint.ID);

            await _viewCustomer.GoToPoint(buyPoint.Position, buyPoint.Rotation);
            
            EventsCustomer.ExecuteCustomerEnterBuyPoint(buyPoint.ID, this , _modelCustomer.DesiredFoodType);

            await UniTask.WaitUntil(() => _modelCustomer.HasFoodStatus);

            _serviceMapPoint.SetEmptyPointWithID(buyPoint.ID);

            await _viewCustomer.GoToPoint(_modelCustomer.StartPosition, _modelCustomer.StartRotation);

            _modelCustomer.SetFoodStatus(false); //переиспользую обьект через Object Pool

            EventsCustomer.ExecuteCustomerLeft(this);
        }
        
        private void GetFood(string pointId, ControllerCustomer controllerCustomer)
        {
            if (controllerCustomer != this) return;

            _modelCustomer.SetFoodStatus(true);
        }
    }
}