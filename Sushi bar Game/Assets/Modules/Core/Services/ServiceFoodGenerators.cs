using System.Collections.Generic;
using System.Linq;
using Modules.Content.Food_Generator;
using Modules.Content.FoodCollection;

namespace Modules.Core.Services
{
    public class ServiceFoodGenerators
    {
        private List<FoodGenerator> _foodGenerators = new();
        
        public void AddNewFoodGenerator(FoodGenerator foodGenerator) => _foodGenerators.Add(foodGenerator);
         
        public FoodGenerator GetFreeFoodGeneratorByFoodType(FoodType foodType)
        {
            return _foodGenerators.FirstOrDefault(x => x.FoodType == foodType);
        }
    }
}