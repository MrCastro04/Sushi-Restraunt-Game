using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using UnityEngine;
using Zenject;

namespace Modules.Features.Characters.Customer
{
    public class Customer : BaseCharacterMover
    {
        [Inject] private PointMono _buyPoint;
        [Inject] Employer.Code.Employer employer;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private bool _getFood = false;

        protected override void Awake()
        {
            base.Awake();

            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            employer.OnSellFood += () => _getFood = true;
        }

        private async void Start()
        {
            await MoveTo(_buyPoint.Position, _buyPoint.Rotation);
            
            EventsCustomer.ExecuteCustomerGetBuyPoint();

            await UniTask.WaitUntil(() => _getFood);

            await MoveTo(_startPosition, _startRotation);
        }
    }
}