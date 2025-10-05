namespace Modules.Content.FoodCollection
{
    public struct Food
    {
        private FoodType _foodType;
        public FoodType FoodType => _foodType;

        public Food(FoodType foodType)
        {
            _foodType = foodType;
        }
    }
}