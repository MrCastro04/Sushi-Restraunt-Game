using Modules.Core.Services;
using Modules.New;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerServices : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServiceMapPoint();

            BindServiceCustomerQueue();

            Container
                .BindInterfacesAndSelfTo<ServiceFoodGenerators>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ServicePlayerResources>()
                .AsSingle();
        }

        private void BindServiceCustomerQueue()
        {
            Container
                .BindInterfacesAndSelfTo<ServiceCustomerQueue>()
                .AsSingle();
        }

        private void BindServiceMapPoint()
        {
            Container
                .BindInterfacesAndSelfTo<ServiceMapPoint>()
                .AsSingle();
        }
    }
}