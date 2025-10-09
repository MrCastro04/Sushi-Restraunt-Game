using Modules.Content.Characters.Customer;
using Modules.Content.Characters.Customer.Controller;
using Modules.Core.Factories;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerFactories : MonoInstaller
    {
        [SerializeField] private ControllerCustomer controllerCustomer;

        public override void InstallBindings()
        {
            Container
                .BindInstance(this)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<FactoryCustomer>()
                .AsSingle()
                .WithArguments(controllerCustomer);
        }
    }
}