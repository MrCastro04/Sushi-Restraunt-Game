using System.Collections.Generic;
using System.Linq;
using Modules.Content.Characters.Customer.Code.Controller;
using Modules.Core.Extensions.GameObject_Extention;
using Modules.Core.Factories;
using UnityEngine;

namespace Modules.Core.Pools
{
    public class PoolCustomer
    {
        private readonly FactoryCustomer _factoryCustomer;
        private readonly Queue<ControllerCustomer> _customersInPool = new();

        public PoolCustomer(FactoryCustomer factoryCustomer, Vector3 spawnPosition, int poolSize = 10)
        {
            _factoryCustomer = factoryCustomer;

            PopulatePool(poolSize, spawnPosition);
        }

        public void PopulatePool(int size, Vector3 spawnPosition)
        {
            for (int i = 0; i < size; i++)
            {
                var customer = _factoryCustomer.CreateItemIn(spawnPosition, Quaternion.identity);

                _customersInPool.Enqueue(customer);

                customer.gameObject.SetActive(false);
            }
        }

        public ControllerCustomer GetIn(Vector3 position, Quaternion rotation)
        {
            if (_customersInPool.Any() == false)
                return null;

            var customer = _customersInPool.Dequeue();

            customer.gameObject.SetTransformValues(position, rotation);

            customer.gameObject.SetActive(true);

            customer.WorkFlow();

            return customer;
        }

        public void Return(ControllerCustomer controllerCustomer)
        {
            _customersInPool.Enqueue(controllerCustomer);

            controllerCustomer.gameObject.SetActive(false);
        }
    }
}