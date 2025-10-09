using System;
using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Employer;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using Modules.Content.UI.Circle_Loading.Code;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.New
{
    public class FoodGenerator : MonoBehaviour
    {
        [Inject] private ServiceMapPoint _serviceMapPoint;
        [Inject] private ServiceFoodGenerators _serviceFoodGenerators;

        [SerializeField] private GameObject _foodPrefab;
        [SerializeField] private PointMono _pointMono;
        [SerializeField] private FoodType _foodTypeGenerates;
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _offsetForLoadingCirclePosition;
        [SerializeField] private float _generateTime;

        private int _profit;

        public PointMono PointMono => _pointMono;
        public FoodType FoodType => _foodTypeGenerates;

        public void ChangeProfitValue(int newProfitValue) => _profit = newProfitValue;

        #region Initialize

        private void Awake()
        {
            _serviceFoodGenerators.AddNewFoodGenerator(this);

            _serviceMapPoint.AddNewPoint(_pointMono, PointType.GatheringFood, _foodTypeGenerates);
        }

        #endregion

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

    public static class EventsEmployer
    {
        public static event Action<ControllerEmployer,FoodType> OnEmployerStartCook;

        public static void ExecuteEventOnStartCook(ControllerEmployer controllerEmployer,FoodType foodType)
        {
            OnEmployerStartCook?.Invoke(controllerEmployer,foodType);
        }
    }
}