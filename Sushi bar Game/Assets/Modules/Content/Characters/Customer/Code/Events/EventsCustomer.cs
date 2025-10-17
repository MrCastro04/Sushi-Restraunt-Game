using System;
using Modules.Content.Characters.Customer.Code.Controller;
using Modules.Content.FoodCollection;

namespace Modules.Content.Characters.Customer.Code.Events
{
    public static class EventsCustomer
    {
        public static event Action<ControllerCustomer> OnLeft;
        public static event Action<string, ControllerCustomer, FoodType> OnEnterBuyPoint;
        public static event Action<string, ControllerCustomer> OnGetFood;

        public static void ExecuteCustomerLeft(ControllerCustomer controllerCustomer)
        {
            OnLeft?.Invoke(controllerCustomer);
        }

        public static void ExecuteCustomerEnterBuyPoint(string pointID, ControllerCustomer controllerCustomer, FoodType foodType)
        {
            OnEnterBuyPoint?.Invoke(pointID, controllerCustomer, foodType);
        }

        public static void ExecuteCustomerGetFood(string pointID, ControllerCustomer controllerCustomer)
        {
            OnGetFood?.Invoke(pointID, controllerCustomer);
        }
    }
}