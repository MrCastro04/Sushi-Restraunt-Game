using Modules.Content.Characters.Employer;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerEmployer : MonoInstaller
    {
        [SerializeField] private ControllerEmployer controllerEmployer;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(controllerEmployer)
                .AsSingle();
        }
    }
}