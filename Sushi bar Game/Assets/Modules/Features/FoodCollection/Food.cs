using System;

namespace Modules.Features.FoodCollection
{
    public class Food
    {
        private FoodType _foodType;
        public FoodType FoodType => _foodType;

        public Food(FoodType foodType)
        {
            _foodType = foodType;
        }
    }
}