using Modules.Content.Characters.Employer.Code;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerEmployer : MonoInstaller
    {
        [SerializeField] private Employer _employer;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_employer)
                .AsSingle();
        }
    }
}