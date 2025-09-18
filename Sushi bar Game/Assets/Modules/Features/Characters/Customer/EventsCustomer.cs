using System;
using Sirenix.OdinInspector.Editor.Internal;

namespace Modules.Features.Characters.Customer
{
    public static class EventsCustomer
    {
        public static event Action OnCustomerGetBuyPoint;

        public static void ExecuteCustomerGetBuyPoint()
        {
            OnCustomerGetBuyPoint?.Invoke();
        }
    }
}