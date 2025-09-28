using System.Collections.Generic;
using System.Linq;
using Modules.Content.Characters.Customer;
using Modules.Features.Characters.Customer;
using Modules.Features.Map_Points;
using UnityEngine;
using Zenject;

namespace Modules.Core.Services
{
    public class ServiceCustomerQueue
    {
        [Inject] private ServiceMapPoint _serviceMapPoint;

        private Queue<Customer> _customers = new();
        private Queue<PointMono> _buyPoints = new();

        public Customer GetPeekCustomer()
        {
            return _customers.Peek();
        }

        public string GetPeekCustomerID()
        {
           var item = _buyPoints.Peek();

           return item.ID;
        }

        public bool IsContainsCustomer(Customer customer)
        {
            return _customers.Contains(customer);
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

        public void AddNewCustomer(string pointID, Customer customer)
        {
            _customers.Enqueue(customer);
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