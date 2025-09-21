using Modules.Core.Services;
using Modules.Features;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerServices : MonoInstaller
    {
        [SerializeField] private PointMono[] _basePoints;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ServiceMapPoint>()
                .AsSingle()
                .WithArguments(_basePoints);

            Container
                .BindInterfacesAndSelfTo<ServiceCustomerQueue>()
                .AsSingle();
        }
    }
}