using Modules.Features;
using UnityEngine;
using Zenject;

namespace Modules.Core
{
    public class InstallerMapPoints : MonoInstaller
    {
        [SerializeField] private PointMono _buyPointMono;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_buyPointMono)
                .AsSingle();
        }
    }
}