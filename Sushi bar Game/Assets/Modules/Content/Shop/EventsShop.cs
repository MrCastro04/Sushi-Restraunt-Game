using System;

namespace Modules.Content.Shop
{
    public static class EventsShop
    {
        public static event Action<string> OnItemPurchasedSuccessfully;
        public static void ExecuteEventOnConfirmPurchase(string itemID, A) => OnItemPurchasedSuccessfully?.Invoke(itemID);
    }
}