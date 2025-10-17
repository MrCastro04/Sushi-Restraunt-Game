using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Content.Food_Generator;
using Modules.Content.FoodCollection;
using Modules.Content.Item;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerFoodGenerator : IInitializable, IDisposable
    {
        private List<FoodGenerator> _foodGenerators;

        public ManagerFoodGenerator(List<FoodGenerator> foodGenerators)
        {
            _foodGenerators = foodGenerators.Any() == false ? new() : foodGenerators;
        }

        public void Initialize()
        {
            EventsItem.OnGetFasterFoodGenerator += ReduceFoodGeneratorGenerateTime;
        }

        public void Dispose()
        {
            EventsItem.OnGetFasterFoodGenerator -= ReduceFoodGeneratorGenerateTime;
        }

        private void ReduceFoodGeneratorGenerateTime(FoodType foodType)
        {
          var generatorsWithType = _foodGenerators.Where(x=> x.FoodType == foodType);

          foreach (var generator in generatorsWithType)
              generator.ReduceGenerateTime(1);
        }
    }
}