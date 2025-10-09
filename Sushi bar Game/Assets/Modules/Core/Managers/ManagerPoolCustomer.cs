using System;
using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Customer;
using Modules.Content.Characters.Customer.Controller;
using Modules.Core.Factories;
using Modules.Core.Pools;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerPoolCustomer : IInitializable, IDisposable
    {
        private readonly PoolCustomer _poolCustomer;
        private readonly ServiceMapPoint _serviceMapPoint;
        private readonly Vector3 _spawnPosition;
        private readonly int _startSpawnCustomerCount = 2;
    
        public ManagerPoolCustomer(
            FactoryCustomer factoryCustomer
            , ServiceMapPoint serviceMapPoint
            , Vector3 spawnPosition)
        {
            _spawnPosition = spawnPosition;

            _serviceMapPoint = serviceMapPoint;
        
            _poolCustomer = new(factoryCustomer, _spawnPosition,_startSpawnCustomerCount);

            SpawnFirstCustomer();
        }

        public void Initialize()
        {
            EventsCustomer.OnGetFood += CleanPointAndSpawnNewCustomer;
            EventsCustomer.OnLeft += _poolCustomer.Return;
        }

        public void Dispose()
        {
            EventsCustomer.OnGetFood -= CleanPointAndSpawnNewCustomer;
            EventsCustomer.OnLeft -= _poolCustomer.Return;
        }

        private void CleanPointAndSpawnNewCustomer(string pointID,ControllerCustomer controllerCustomer)
        {
            _serviceMapPoint.SetEmptyPointWithID(pointID);
        
            _poolCustomer.GetIn(_spawnPosition, Quaternion.identity);
        }

        private async void SpawnFirstCustomer()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _poolCustomer.GetIn(_spawnPosition, Quaternion.identity);
        }
    }
}