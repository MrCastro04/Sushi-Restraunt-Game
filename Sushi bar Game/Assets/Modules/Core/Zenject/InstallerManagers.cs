using System.Collections.Generic;
using Modules.Content.Food_Generator;
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
        [SerializeField] private List<FoodGenerator> _foodGenerators;
        [SerializeField] private List<DataItem> _dataItems;
        [SerializeField] private ViewItem _viewItemPrefab;
        [SerializeField] private ViewShop _viewShop;
        [SerializeField] private Transform _employerSpawnTransform;
        
        public override void InstallBindings()
        {
            BindManagerShop();

            Container
                .BindInterfacesAndSelfTo<ManagerFoodGenerator>()
                .AsSingle()
                .WithArguments(_foodGenerators);
            
            BindManagerEmployer();

            BindManagerCustomer();

            BindManagerScreen();
        }

        private void BindManagerEmployer()
        {
            Container
                .BindInterfacesAndSelfTo<ManagerEmployer>()
                .AsSingle()
                .WithArguments(_employerSpawnTransform.position);
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

        private void BindManagerCustomer()
        {
            var resolveCollectionMapPoints = Container.Resolve<CollectionPointsMono>();

            // взять позицию точки с ID - CSP1 ( "Customer Spawn 1").
            var postionDefaultSpawnPoint = resolveCollectionMapPoints.MapPoints["CSP1"].PointMono.Position;
            Container
                .BindInterfacesAndSelfTo<ManagerCustomer>()
                .AsSingle()
                .WithArguments(postionDefaultSpawnPoint);
        }
    }
}