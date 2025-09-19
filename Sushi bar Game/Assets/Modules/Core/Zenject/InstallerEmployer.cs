using Modules.Features.Characters.Employer.Code;
using UnityEngine;
using Zenject;

namespace Modules.Core
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