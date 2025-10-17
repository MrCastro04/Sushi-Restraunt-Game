using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Employer.Code.Controller;
using Modules.Content.Characters.Employer.Events;
using Modules.Content.Characters.Employer.View;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using Modules.Content.UI.Circle_Loading.Code;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Content.Food_Generator
{
    public class FoodGenerator : MonoBehaviour
    {
        [SerializeField] private ViewFood _viewFood;
        [SerializeField] private PointMono _pointMono;
        [SerializeField] private FoodType _foodTypeGenerates;
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _offsetForLoadingCirclePosition;
        [SerializeField] private float _generateTime;

        private ServiceMapPoint _serviceMapPoint; 
        private ServiceFoodGenerators _serviceFoodGenerators;
        private int _currentProfit = 200;
        
        public ViewFood ViewFood => _viewFood;
        public PointMono PointMono => _pointMono;
        public FoodType FoodType => _foodTypeGenerates;
        public int CurrentProfit => _currentProfit;
        public float GenerateTime => _generateTime;

        [Inject]
        private void Construct(ServiceMapPoint serviceMapPoint, ServiceFoodGenerators serviceFoodGenerators)
        {
            _serviceMapPoint = serviceMapPoint;

            _serviceFoodGenerators = serviceFoodGenerators;
            
            Init();
        }
        
        private void Init()
        {
            _serviceFoodGenerators.AddNewFoodGenerator(this);

            _serviceMapPoint.AddNewPoint(_pointMono, PointType.GatheringFood, _foodTypeGenerates);
            
            CreateFood();
            
            Debug.Log($"{_generateTime}");
        }

        public void ReduceGenerateTime(float newGenerateTime)
        {
            _generateTime -= newGenerateTime;
            
            Debug.Log($"{_generateTime}");
        }
        public void AddProfitValue(int newProfitValue) => _currentProfit += newProfitValue;

        public void CreateFood()
        {
          _viewFood = Instantiate(_viewFood, transform.position, Quaternion.identity); 
          
          _viewFood.Hide();
        }

        public ViewFood GetViewFood(ControllerEmployer employer , Vector3 viewFoodPosition)
        {
            _viewFood.Show();
            
            _viewFood.DisplayProfitText(_currentProfit);
            
            _viewFood.transform.SetParent(employer.transform);

            _viewFood.transform.position = viewFoodPosition;
            
            return _viewFood;
        }

        public async UniTask StartUse(ControllerEmployer controllerEmployer)
        {
            if (_loadingCircle.gameObject.activeSelf)
                _loadingCircle.gameObject.SetActive(false);

            var newLoadingCirclePosition = _pointMono.Position.y + _offsetForLoadingCirclePosition;

            _loadingCircle.transform.position = new Vector3(
                _pointMono.transform.position.x
                , newLoadingCirclePosition
                , _pointMono.transform.position.z);

            _loadingCircle.gameObject.SetActive(true);
            
            EventsEmployer.ExecuteEventOnStartCook(controllerEmployer,_foodTypeGenerates);
            
            await _loadingCircle.RunImmitation(_generateTime);
        }
    }
}