using System.Collections.Generic;
using System.Linq;
using Modules.Core.Factories;
using Modules.Features.Characters.Customer;
using UnityEngine;

namespace Modules.@new
{
    public class PoolCustomer
    {
        private readonly FactoryCustomer _factoryCustomer;
        private readonly Queue<Customer> _customersInPool = new();
        
        public PoolCustomer(FactoryCustomer factoryCustomer, Vector3 spawnPosition, int poolSize = 10)
        {
            _factoryCustomer = factoryCustomer;
            
            PopulatePool(poolSize, spawnPosition);
        }

        private void PopulatePool(int size, Vector3 spawnPosition)
        {
            for (int i = 0; i < size; i++)
            {
                var customer = _factoryCustomer.CreateItemIn(spawnPosition, Quaternion.identity);
                 
                _customersInPool.Enqueue(customer);
       
                customer.gameObject.SetActive(false);
            }
        }

        public Customer GetIn(Vector3 position, Quaternion rotation)
        {
            if (_customersInPool.Any() == false)
                return null;
            
            var customer = _customersInPool.Dequeue();

            customer.gameObject.SetTransformValues(position,rotation);
      
            customer.gameObject.SetActive(true);

            return customer;
        }

        public void Return(Customer customer)
        {
            _customersInPool.Enqueue(customer);
        
            customer.gameObject.SetActive(false);
        }
    }
}