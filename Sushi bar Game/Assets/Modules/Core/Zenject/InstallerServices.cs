using Modules.Core.Services;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerServices : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ServiceMapPoint>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ServiceCustomerQueue>()
                .AsSingle();
        }
    }
}