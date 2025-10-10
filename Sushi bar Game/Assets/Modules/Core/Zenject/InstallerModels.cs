using Modules.Content.Characters.Customer.Model;
using Modules.Content.Characters.Employer;
using Modules.Content.Characters.Employer.Model;
using Modules.Content.FoodCollection;
using UnityEngine;
using Zenject;

namespace Modules.Core.Zenject
{
    public class InstallerModels : MonoInstaller
    {
        [SerializeField] private FoodType _foodType;
        [SerializeField] private float _startImmitationTime = 2f;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ModelCustomer>()
                .AsTransient()
                .WithArguments(_foodType);

            Container
                .BindInterfacesAndSelfTo<ModelEmployer>()
                .AsTransient()
                .WithArguments(_startImmitationTime);

        }
    }
}