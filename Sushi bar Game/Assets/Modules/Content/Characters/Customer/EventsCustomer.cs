using System;

namespace Modules.Content.Characters.Customer
{
    public static class EventsCustomer
    {
        public static event Action<Customer> OnLeft;
        public static event Action<string, Customer> OnEnterBuyPoint;
        public static event Action<string, Customer> OnGetFood;

        public static void ExecuteCustomerLeft(Customer customer)
        {
            OnLeft?.Invoke(customer);
        }

        public static void ExecuteCustomerEnterBuyPoint(string pointID, Customer customer)
        {
            OnEnterBuyPoint?.Invoke(pointID, customer);
        }

        public static void ExecuteCustomerGetFood(string pointID, Customer customer)
        {
            OnGetFood?.Invoke(pointID, customer);
        }
    }
}