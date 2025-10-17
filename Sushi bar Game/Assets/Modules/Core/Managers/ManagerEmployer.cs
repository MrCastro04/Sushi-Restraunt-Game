using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Content.Characters.Customer.Controller;
using Modules.Content.Characters.Customer.Events;
using Modules.Content.Characters.Employer.Code.Controller;
using Modules.Content.FoodCollection;
using Modules.Content.Item;
using Modules.Core.Factories;
using Modules.Core.Services;
using UnityEngine;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerEmployer : IInitializable, IDisposable
    {
        private readonly FactoryEmployer _factoryEmployer;
        private readonly ServiceCustomerQueue _serviceCustomerQueue;
        private readonly Vector3 _spawnPosition;
        private readonly List<ControllerEmployer> _employers = new();

        public ManagerEmployer(
            FactoryEmployer factoryEmployer,
            ServiceCustomerQueue serviceCustomerQueue,
            Vector3 spawnPosition)
        {
            _spawnPosition = spawnPosition;
            _factoryEmployer = factoryEmployer;
            _serviceCustomerQueue = serviceCustomerQueue;
        }

        public void Initialize()
        {
            SpawnEmployer();

            EventsItem.OnGetMoreEmployers += SpawnEmployer;

            EventsCustomer.OnEnterBuyPoint += HandleCustomerArrival;
        }

        public void Dispose()
        {
            EventsItem.OnGetMoreEmployers -= SpawnEmployer;

            EventsCustomer.OnEnterBuyPoint -= HandleCustomerArrival;
        }

        private void SpawnEmployer()
        {
            var newEmployer = _factoryEmployer.CreateItemIn(_spawnPosition, Quaternion.identity);

            _employers.Add(newEmployer);
        }

        private void HandleCustomerArrival(string pointID, ControllerCustomer controllerCustomer, FoodType foodType)
        {
            if (_serviceCustomerQueue.IsContainsCustomerID(pointID) ||
                _serviceCustomerQueue.IsContainsCustomer(controllerCustomer))
            {
                return;
            }

            _serviceCustomerQueue.AddNewCustomer(pointID, controllerCustomer, foodType);

            TryAssignFreeEmployer();
        }
        
        private void TryAssignFreeEmployer()
        {
            if (_serviceCustomerQueue.IsQueueEmpty()) return;

            var freeEmployer = _employers.FirstOrDefault(emp => !emp.IsBusy);

            if (freeEmployer == null) return;

            var customerInfo = _serviceCustomerQueue.TryDequeueCustomer();

            if (customerInfo == null) return;

            freeEmployer.StartWorkflow(customerInfo.pointID, customerInfo.customer, customerInfo.foodType,
                TryAssignFreeEmployer);
        }
    }
}