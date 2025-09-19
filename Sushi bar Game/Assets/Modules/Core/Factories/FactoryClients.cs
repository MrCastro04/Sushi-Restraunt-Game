using Modules.Features.Characters.Customer;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Zenject;


namespace Modules.Core 
{
    public class FactoryClients
    {
        private readonly Customer _customerPrefab;
        private readonly DiContainer _diContainer;
        
        public FactoryClients(Customer customerPrefab, DiContainer diContainer)
        {
            _customerPrefab = customerPrefab;

            _diContainer = diContainer;
        }

        public void CreateAt(Vector3 createPosition, Quaternion createRotation, Transform parent = null)
        { 
            _diContainer
                .InstantiatePrefabForComponent<Customer>(_customerPrefab, createPosition, createRotation, parent);
        }
    }
}