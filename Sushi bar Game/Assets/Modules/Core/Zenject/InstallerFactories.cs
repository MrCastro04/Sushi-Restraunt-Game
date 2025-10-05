using Modules.Content.Characters.Customer;
using Modules.Core.Factories;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerFactories : MonoInstaller
    {
        [SerializeField] private Customer _customer;

        public override void InstallBindings()
        {
            Container
                .BindInstance(this)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<FactoryCustomer>()
                .AsSingle()
                .WithArguments(_customer);
        }
    }
}