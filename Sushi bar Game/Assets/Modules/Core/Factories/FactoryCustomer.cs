using Modules.Content.Characters.Customer;
using Modules.Content.Characters.Customer.Controller;
using UnityEngine;
using Zenject;

namespace Modules.Core.Factories
{
    public class FactoryCustomer : IFactory<ControllerCustomer>
    {
        private readonly DiContainer _diContainer;
        private readonly ControllerCustomer _controllerCustomerPrefab;
        
        public FactoryCustomer(DiContainer diContainer, ControllerCustomer controllerCustomerPrefab)
        {
            _diContainer = diContainer;
            _controllerCustomerPrefab = controllerCustomerPrefab;
        }

        public ControllerCustomer CreateItemIn(Vector3 createPosition, Quaternion createRotation, Transform parent = null)
        {
            return _diContainer
                .InstantiatePrefabForComponent<ControllerCustomer>(_controllerCustomerPrefab, createPosition, createRotation, parent);
        }
    }
}