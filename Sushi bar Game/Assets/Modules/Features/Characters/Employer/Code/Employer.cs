using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Core;
using Modules.Features.Characters.Base.Code;
using Modules.Features.Characters.Customer;
using UnityEditor.PackageManager;
using UnityEngine;
using Zenject;

namespace Modules.Features.Characters.Employer.Code
{
    public class Employer : BaseEntity
    {
        [SerializeField] private PointMono _gatheringPoint;
        [SerializeField] private LoadingCircle _loadingCircle;
        [SerializeField] private float _immitationTime;

        [Inject] private ServiceMapPoint _serviceMapPoint;
        [Inject] private ServiceCustomerQueue _serviceCustomerQueue;

        private bool _isBusy = false;

        #region EventSubscription

        private void OnEnable()
        {
            EventsCustomer.OnGetBuyPoint += RunWorkFlow;
        }

        private void OnDisable()
        {
            EventsCustomer.OnGetBuyPoint -= RunWorkFlow;
        }

        #endregion

        private async void RunWorkFlow(string pointID, Customer.Customer customer)
        {
            if (_serviceCustomerQueue.IsContainsCustomerID(pointID) &
                _serviceCustomerQueue.IsContainsCustomer(customer))
            {
                Debug.Log("Такой клиент уже есть в очереди!");
            }
            else
            {
                _serviceCustomerQueue.AddNewCustomer(pointID, customer);
            }

            if (_isBusy)
            {
                Debug.Log("Работник занят. Не может обслуживать");
                return;
            }

            _isBusy = true;

            await WorkFlow(pointID, customer);

            if (_serviceCustomerQueue.IsQueueEmpty() == false)
            {
                Debug.Log("очередь не пустая");
                _isBusy = false;
                RunWorkFlow(_serviceCustomerQueue.GetPeekCustomerID(), _serviceCustomerQueue.GetPeekCustomer());
            }
            else
            {
                Debug.Log("очередь пустая");
                _isBusy = false;
            }
        }

        private async UniTask WorkFlow(string pointID, Customer.Customer customer)
        {
            var sellPoint = _serviceMapPoint.GetNeighboringPointForEmployer(pointID);

            await GoToPoint(sellPoint, true);

            await GoToPoint(_gatheringPoint, true);

            await GoToPoint(sellPoint);

            EventsCustomer.ExecuteCustomerGetFood(customer);

            _serviceCustomerQueue.RemoveCustomer(pointID, customer);
        }

        private async UniTask GoToPoint(PointMono pointMono, bool withImmitation = false)
        {
            await MoveTo(pointMono.Position, pointMono.Rotation);

            if (withImmitation)
                await _loadingCircle.RunImmitation(_immitationTime);
        }
    }
}