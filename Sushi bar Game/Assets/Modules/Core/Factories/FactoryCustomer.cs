using Modules.Content.Characters.Customer.Controller;
using UnityEngine;
using Zenject;

namespace Modules.Core.Factories
{
    public class FactoryCustomer : IFactory<ControllerCustomer>
    {
        private readonly DiContainer _diContainer;
        private readonly ControllerCustomer _controllerEmployerPrefab;

        public FactoryCustomer(DiContainer diContainer, ControllerCustomer controllerEmployerPrefab)
        {
            _diContainer = diContainer;
            
            _controllerEmployerPrefab = controllerEmployerPrefab;
        }

        public ControllerCustomer CreateItemIn(Vector3 createPosition, Quaternion createRotation, Transform parent = null)
        {
            return _diContainer
                .InstantiatePrefabForComponent<ControllerCustomer>(_controllerEmployerPrefab, createPosition, createRotation, parent);
        }
    }
}