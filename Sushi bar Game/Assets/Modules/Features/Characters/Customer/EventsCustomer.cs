using System;
using Cysharp.Threading.Tasks;
using UnityEditor.PackageManager;

namespace Modules.Features.Characters.Customer
{
    public static class EventsCustomer
    {
        public static event Action<string,Customer> OnGetBuyPoint;
        public static event Action<string, Customer> OnLeftBuyPoint;
        public static event Action<Customer> OnGetFood;
        
        public static void ExecuteCustomerGetBuyPoint( string pointID,Customer customer)
        {
            OnGetBuyPoint?.Invoke(pointID, customer);
        }
        public static void ExecuteCustomerLeftBuyPoint(string pointID,Customer customer)
        {
            OnLeftBuyPoint?.Invoke(pointID, customer);
        }


        public static void ExecuteCustomerGetFood(Customer customer)
        {
            OnGetFood?.Invoke(customer);
        }
    }
}