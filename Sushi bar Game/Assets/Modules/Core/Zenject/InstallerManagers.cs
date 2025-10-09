using Modules.Core.Managers;
using Modules.Core.Serializeable_Collections.Map_Points;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerManagers : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindManagerCustomerQueue();

            BindManagerScreen();
        }

        private void BindManagerScreen()
        {
            Container
                .BindInterfacesAndSelfTo<ManagerScreen>()
                .AsSingle();
        }

        private void BindManagerCustomerQueue()
        {
            var resolveCollectionMapPoints = Container.Resolve<CollectionPointsMono>();

            // взять позицию точки с ID - CS1 ( "Customer Spawn 1") .
            var postionDefaultSpawnPoint = resolveCollectionMapPoints.MapPoints["CS1"].PointMono.Position;

            Container
                .BindInterfacesAndSelfTo<ManagerPoolCustomer>()
                .AsSingle()
                .WithArguments(postionDefaultSpawnPoint);
        }
    }
}