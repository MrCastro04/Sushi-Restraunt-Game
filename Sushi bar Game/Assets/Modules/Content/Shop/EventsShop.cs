using System;

namespace Modules.Content.Shop
{
    public static class EventsShop
    {
        // public static event Action<string> OnCollectItem;
        public static event Action<string> OnItemPurchasedSuccessfully;

        // public static void ExecuteEventOnCollectItem(string itemID) => OnCollectItem?.Invoke(itemID);
        public static void ExecuteEventOnItemPurchased(string itemID) => OnItemPurchasedSuccessfully?.Invoke(itemID);
    }
}