using System.Collections.Generic;
using System.Linq;
using Modules.Content.Characters.Customer;
using Modules.Content.Characters.Customer.Controller;
using Modules.Content.FoodCollection;
using Modules.Content.Map_Points;
using UnityEngine;
using Zenject;

namespace Modules.Core.Services
{
    public class ServiceCustomerQueue
    {
        [Inject] private ServiceMapPoint _serviceMapPoint;

        private Queue<CustomerQueueInfo> _queuedCustomers = new();

        public int QueueCount => _queuedCustomers.Count;

        public bool IsContainsCustomer(ControllerCustomer controllerCustomer)
        {
            return _queuedCustomers.Any(info => info.customer == controllerCustomer);
        }

        public bool IsContainsCustomerID(string customerID)
        {
            return _queuedCustomers.Any(info => info.pointID == customerID);
        }

        public bool IsQueueEmpty() => _queuedCustomers.Count == 0;

        public void AddNewCustomer(string pointID, ControllerCustomer controllerCustomer, FoodType foodType)
        {
            var newCustomerInfo = new CustomerQueueInfo
            {
                pointID = pointID,
                customer = controllerCustomer,
                foodType = foodType
            };

            _queuedCustomers.Enqueue(newCustomerInfo);
        }

        public CustomerQueueInfo TryDequeueCustomer()
        {
            if (_queuedCustomers.Count == 0) return null;

            return _queuedCustomers.Dequeue();
        }
    }

    public class CustomerQueueInfo
    {
        public string pointID;
        public ControllerCustomer customer;
        public FoodType foodType;
    }
}