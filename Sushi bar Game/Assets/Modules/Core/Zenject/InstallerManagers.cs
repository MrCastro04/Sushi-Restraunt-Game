using System.Collections.Generic;
using Modules.Content.Item;
using Modules.Content.Shop;
using Modules.Core.Managers;
using Modules.Core.Serializeable_Collections.Map_Points;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerManagers : MonoInstaller
    {
        [SerializeField] private List<DataItem> _dataItems;
        [SerializeField] private ViewItem _viewItemPrefab;
        [SerializeField] private ViewShop _viewShop;
        
        public override void InstallBindings()
        {
            BindManagerShop();

            BindManagerCustomerQueue();

            BindManagerScreen();
        }

        private void BindManagerShop()
        {
            Container
                .BindInterfacesAndSelfTo<ManagerShop>()
                .AsSingle()
                .WithArguments(_dataItems, _viewItemPrefab, _viewShop)
                .NonLazy();
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
                .BindInterfacesAndSelfTo<ManagerCustomer>()
                .AsSingle()
                .WithArguments(postionDefaultSpawnPoint);
        }
    }
}