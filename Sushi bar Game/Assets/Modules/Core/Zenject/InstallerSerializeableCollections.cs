using Modules.Core.Serializeable_Collections.Map_Points;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerSerializeableCollections : MonoInstaller
    {
        [SerializeField] private CollectionPointsMono collectionPointsMono;
        
        public override void InstallBindings()
        {
            BindCollectionMapPoints();
        }

        private void BindCollectionMapPoints()
        {
            Container
                .BindInstance(collectionPointsMono)
                .AsSingle();

            var resolve = Container.Resolve<CollectionPointsMono>();
            resolve.Init();
        }
    }
}