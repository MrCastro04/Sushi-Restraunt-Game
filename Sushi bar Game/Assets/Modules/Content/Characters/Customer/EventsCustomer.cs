using System;

namespace Modules.Features.Characters.Customer
{
    public static class EventsCustomer
    {
        public static event Action<Content.Characters.Customer.Customer> OnLeft;
        public static event Action<string,Content.Characters.Customer.Customer> OnGetBuyPoint;
        public static event Action<string,Content.Characters.Customer.Customer> OnGetFood;

        public static void ExecuteCustomerLeft(Content.Characters.Customer.Customer customer)
        {
            OnLeft?.Invoke(customer);
        }
        
        public static void ExecuteCustomerGetBuyPoint( string pointID,Content.Characters.Customer.Customer customer)
        {
            OnGetBuyPoint?.Invoke(pointID, customer);
        }

        public static void ExecuteCustomerGetFood(string pointID,Content.Characters.Customer.Customer customer)
        {
            OnGetFood?.Invoke(pointID,customer);
        }
    }
}