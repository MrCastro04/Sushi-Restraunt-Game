using System;
using Modules.Content.Player_Resources;
using Modules.Content.Player_Resources.View;
using Modules.Content.Player_Resources.View_Model;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerViewModels : MonoInstaller
    {
        [SerializeField] private ViewPlayerResources _viewPlayerResources;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ViewModelPlayerResources>()
                .AsSingle()
                .WithArguments(_viewPlayerResources);
        }
    }
}