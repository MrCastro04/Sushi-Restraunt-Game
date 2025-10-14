using Modules.Content.Characters.Customer.Controller;
using Modules.Content.Characters.Employer.Controller;
using Modules.Core.Factories;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerFactories : MonoInstaller
    {
        [SerializeField] private ControllerCustomer controllerCustomer;
        [SerializeField] private ControllerEmployer _controllerEmployerPrefab;
        public override void InstallBindings()
        {
            BindDiContainer();

            BindFactoryCustomer();

            BindFactoryEmployer();
        }

        private void BindDiContainer()
        {
            Container
                .BindInstance(this)
                .AsSingle();
        }

        private void BindFactoryCustomer()
        {
            Container
                .BindInterfacesAndSelfTo<FactoryCustomer>()
                .AsSingle()
                .WithArguments(controllerCustomer);
        }

        private void BindFactoryEmployer()
        {
            Container
                .BindInterfacesAndSelfTo<FactoryEmployer>()
                .AsSingle()
                .WithArguments(_controllerEmployerPrefab);
        }
    }
}