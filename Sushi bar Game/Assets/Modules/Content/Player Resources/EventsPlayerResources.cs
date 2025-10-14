using System;
using Modules.Content.Item;

namespace Modules.Content.Player_Resources
{
    public static class EventsPlayerResources
    {
        public static event Action<ModelItem> OnTryBuyItem;
        public static void ExecuteEventOnTryBuyItem(ModelItem modelItem) => OnTryBuyItem?.Invoke(modelItem);
    }
}