using System;
using Modules.Content.Item;

namespace Modules.Content.Player_Resources
{
    public static class EventsPlayerResources
    {
        public static event Action<int, string> OnTryBuyItem;
        public static void ExecuteEventOnTryBuyItem(int costItem, string IdItem) => OnTryBuyItem?.Invoke(costItem, IdItem);
    }
}