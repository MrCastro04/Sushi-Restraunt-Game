using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerManagers : MonoInstaller
    {
        [SerializeField] private Transform _customerSpawnPoint;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ManagerCustomerQueue>()
                .AsSingle()
                .WithArguments(_customerSpawnPoint.position);
        }
    }
}