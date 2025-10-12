using Modules.Core.Services;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerServices : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServiceMapPoint();

            BindServiceCustomerQueue();

            BindServiceFoodGenerators();
        }

        private void BindServiceFoodGenerators()
        {
            Container
                .BindInterfacesAndSelfTo<ServiceFoodGenerators>()
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