

using System;
using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using UnityEngine;

namespace Modules.Features.Characters.Client
{
    public class Customer : BaseCharacterMover
    {
        [SerializeField] private PointMono _buyPoint;

        [SerializeField] private Worker.Code.Worker _worker;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private bool _getFood = false;

        public event Action OnGetDestination;

        protected override void Awake()
        {
            base.Awake();

            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            _worker.OnSellFood += () => _getFood = true;
        }

        private async void Start()
        {
            await MoveTo(_buyPoint.Position, _buyPoint.Rotation);

            OnGetDestination?.Invoke();
            
            await UniTask.WaitUntil(() => _getFood);

            await MoveTo(_startPosition, _startRotation);
        }
    }
}