using Modules.Content.Characters.Employer.Controller;
using UnityEngine;
using Zenject;

namespace Modules.Core.Factories 
{
    public class FactoryEmployer : IFactory<ControllerEmployer>
    {
        private readonly DiContainer _diContainer;
        private readonly ControllerEmployer _controllerEmployerPrefab;

        public FactoryEmployer(DiContainer diContainer, ControllerEmployer controllerEmployerPrefab)
        {
            _diContainer = diContainer;
            
            _controllerEmployerPrefab = controllerEmployerPrefab;
        }

        public ControllerEmployer CreateItemIn(Vector3 createPosition, Quaternion createRotation, Transform parent = null)
        {
            return _diContainer
                .InstantiatePrefabForComponent<ControllerEmployer>(_controllerEmployerPrefab, createPosition, createRotation, parent);
        }
    }
}