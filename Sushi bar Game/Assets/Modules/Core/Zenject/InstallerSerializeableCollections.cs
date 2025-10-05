using System;
using Modules.Core.Serializeable_Collections.Map_Points;
using Modules.Core.Serializeable_Collections.Screens;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerSerializeableCollections : MonoInstaller
    {
        [SerializeField] private CollectionPointsMono _collectionPointsMono;
        [SerializeField] private CollectionScreens _collectionScreens;
        
        public override void InstallBindings()
        {
            BindCollectionMapPoints();

            BindCollectionScreen();
        }

        private void BindCollectionScreen()
        {
            Container
                .BindInstance(_collectionScreens)
                .AsSingle();
        }

        private void BindCollectionMapPoints()
        {
            Container
                .BindInstance(_collectionPointsMono)
                .AsSingle();

            var resolve = Container.Resolve<CollectionPointsMono>();
            resolve.Init();
        }
    }
}