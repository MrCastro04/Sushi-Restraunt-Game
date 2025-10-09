using System.Collections.Generic;
using System.Linq;
using Modules.Content.Characters.Customer;
using Modules.Content.Characters.Customer.Controller;
using Modules.Content.Map_Points;
using UnityEngine;
using Zenject;

namespace Modules.Core.Services
{
    public class ServiceCustomerQueue
    {
        [Inject] private ServiceMapPoint _serviceMapPoint;

        private Queue<ControllerCustomer> _customers = new();
        private Queue<PointMono> _buyPoints = new();

        public ControllerCustomer GetPeekCustomer()
        {
            return _customers.Peek();
        }

        public string GetPeekCustomerID()
        {
           var item = _buyPoints.Peek();

           return item.ID;
        }

        public bool IsContainsCustomer(ControllerCustomer controllerCustomer)
        {
            return _customers.Contains(controllerCustomer);
        }

        public bool IsContainsCustomerID(string customerID)
        {
            return _buyPoints.FirstOrDefault(x => x.ID == customerID);
        }

        public bool IsQueueEmpty()
        {
            if (_customers.Any())
                return false;

            return true;
        }

        public void AddNewCustomer(string pointID, ControllerCustomer controllerCustomer)
        {
            _customers.Enqueue(controllerCustomer);
            _buyPoints.Enqueue(_serviceMapPoint.GetFreePointByID(pointID));
            
            Debug.Log($"Пришел посититель. Посетителей в колекции - {_customers.Count}");
        }

        public void RemoveCurrentCustomer()
        {
            _customers.Dequeue();

            _buyPoints.Dequeue();
            
            Debug.Log($" Посетитель ушел. Посетителей в колекции - {_customers.Count}");
        }
    }
}