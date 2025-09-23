using Modules.Core.Serializeable_Collections.Map_Points;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerSerializeableCollections : MonoInstaller
    {
        [SerializeField] private CollectionMapPoints _collectionMapPoints;
        
        public override void InstallBindings()
        {
            BindCollectionMapPoints();
        }

        private void BindCollectionMapPoints()
        {
            Container
                .BindInstance(_collectionMapPoints)
                .AsSingle();

            var resolve = Container.Resolve<CollectionMapPoints>();
            resolve.Init();
        }
    }
}