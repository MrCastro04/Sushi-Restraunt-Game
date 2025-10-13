using System;
using System.Collections.Generic;
using Modules.Content.Item;
using Modules.Content.UI.Screens.Base;
using Modules.Core.Managers;
using Modules.Core.Serializeable_Collections.Map_Points;
using Modules.Core.Serializeable_Collections.Screens;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerManagers : MonoInstaller
    {
        [SerializeField] private List<DataItem> _dataItems;
        [SerializeField] private ViewItem _viewItemPrefab;

        public override void InstallBindings()
        {
            BindManagerShop();

            BindManagerCustomerQueue();

            BindManagerScreen();
        }

        private void BindManagerShop()
        {
            var collectionScreensResolve = Container.Resolve<CollectionScreens>();

            var shopScreen = collectionScreensResolve.Screens[ScreenType.Shop];

            Container
                .BindInterfacesAndSelfTo<ManagerShop>()
                .AsSingle()
                .WithArguments(_dataItems, _viewItemPrefab, shopScreen)
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