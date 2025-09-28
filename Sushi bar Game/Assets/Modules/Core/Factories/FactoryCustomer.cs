using Modules.Content.Characters.Customer;
using Modules.Features.Characters.Customer;
using UnityEngine;
using Zenject;

namespace Modules.Core.Factories
{
    public class FactoryCustomer : IFactory<Customer>
    {
        private readonly DiContainer _diContainer;
        private readonly Customer _customerPrefab;
        
        public FactoryCustomer(DiContainer diContainer, Customer customerPrefab)
        {
            _diContainer = diContainer;
            _customerPrefab = customerPrefab;
        }

        public Customer CreateItemIn(Vector3 createPosition, Quaternion createRotation, Transform parent = null)
        {
            return _diContainer
                .InstantiatePrefabForComponent<Customer>(_customerPrefab, createPosition, createRotation, parent);
        }
    }
}