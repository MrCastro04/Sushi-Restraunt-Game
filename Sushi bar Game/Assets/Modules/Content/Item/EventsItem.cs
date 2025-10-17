using System;
using Modules.Content.FoodCollection;

namespace Modules.Content.Item
{
    public static class EventsItem
    {
        public static event Action OnGetMoreEmployers;
        public static event Action OnGetMoreCustomers;
        public static event Action<FoodType> OnGetFasterFoodGenerator;

        public static void ExecuteEventOnGetMoreEmployers()
            => OnGetMoreEmployers?.Invoke();
        
        public static void ExecuteEventOnGetMoreCustomers()
            => OnGetMoreCustomers?.Invoke();

        public static void ExecuteEventOnGetFasterFoodGenerator(FoodType foodType) 
            => OnGetFasterFoodGenerator?.Invoke(foodType);
    }
}