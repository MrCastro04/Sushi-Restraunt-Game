using System;
using Cysharp.Threading.Tasks;
using Modules.Content.Characters.Customer.Controller;
using Modules.Content.Characters.Customer.Events;
using Modules.Content.FoodCollection;
using Modules.Content.Item;
using Modules.Core.Factories;
using Modules.Core.Pools;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerCustomer : IInitializable, IDisposable
    {
        private readonly PoolCustomer _poolCustomer;
        private readonly Vector3 _spawnPosition;
        private int _spawnCustomerCount = 1;
        private int _spawnDelaySeconds = 2;

        public ManagerCustomer(FactoryCustomer factoryCustomer, Vector3 spawnPosition)
        {
            _spawnPosition = spawnPosition;
            
            _poolCustomer = new(factoryCustomer, _spawnPosition, _spawnCustomerCount);
        }

        public void Initialize()
        {
            SpawnCustomer();
            
            EventsCustomer.OnLeft += ReturnAndSpawn;
            EventsItem.OnGetMoreCustomers += UpgradeSpawnCustomerCount;
        }

        public void Dispose()
        {
            EventsCustomer.OnLeft -= ReturnAndSpawn;
            EventsItem.OnGetMoreCustomers -= UpgradeSpawnCustomerCount;
        }

        private void UpgradeSpawnCustomerCount()
        {
            _spawnCustomerCount++;

            _poolCustomer.PopulatePool(1, _spawnPosition);

            SpawnCustomer(true);
        }

        private void ReturnAndSpawn(ControllerCustomer customer)
        {
            _poolCustomer.Return(customer);
            
            SpawnCustomer(true);
        }
        
        private async void SpawnCustomer(bool withDelay = false)
        {
            if (withDelay)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelaySeconds));
                
                _poolCustomer.GetIn(_spawnPosition, Quaternion.identity);
            }
            else
            {
                _poolCustomer.GetIn(_spawnPosition, Quaternion.identity);
            }
        }
    }
}