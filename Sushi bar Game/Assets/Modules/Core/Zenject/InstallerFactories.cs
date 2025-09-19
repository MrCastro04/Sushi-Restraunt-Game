using Modules.Features.Characters.Customer;
using UnityEngine;
using Zenject;

namespace Modules.Core
{
    public class InstallerFactories : MonoInstaller
    {
        [SerializeField] private Customer _customer;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<FactoryClients>()
                .AsSingle()
                .WithArguments(_customer);
        }
    }
}