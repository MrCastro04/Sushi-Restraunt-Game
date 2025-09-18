using System;
using Modules.Features.Characters.Base.Code;
using UnityEngine;

namespace Modules.Features.Characters.Client
{
    public class Customer : BaseCharacterMover
    {
        [SerializeField] private Transform _buyPoint;

        public event Action OnGetDestination;
        
        private async void Start()
        {
            await MoveTo(_buyPoint);
            
            OnGetDestination?.Invoke();
        }
    }
}