using System;

namespace Modules.Features.Characters.Customer
{
    public static class EventsCustomer
    {
        public static event Action<Customer> OnLeft;
        public static event Action<string,Customer> OnGetBuyPoint;
        public static event Action<string,Customer> OnGetFood;

        public static void ExecuteCustomerLeft(Customer customer)
        {
            OnLeft?.Invoke(customer);
        }
        
        public static void ExecuteCustomerGetBuyPoint( string pointID,Customer customer)
        {
            OnGetBuyPoint?.Invoke(pointID, customer);
        }

        public static void ExecuteCustomerGetFood(string pointID,Customer customer)
        {
            OnGetFood?.Invoke(pointID,customer);
        }
    }
}