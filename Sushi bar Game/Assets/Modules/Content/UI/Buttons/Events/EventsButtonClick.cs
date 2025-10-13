using System;
using Modules.Content.Item;
using Modules.Content.UI.Screens.Base;

namespace Modules.Content.UI.Buttons.Events
{
    public static class EventsButtonClick
    {
        public static event Action<ItemType, int> OnBuyItem; 
        public static event Action<ScreenType> OnOpenScreen;
        public static event Action OnCloseScreen;

        public static void ExecuteEventOnOpenScreen(ScreenType screenType)
        {
            OnOpenScreen?.Invoke(screenType);
        }
    
        public static void ExecuteEventOnCloseScreen()
        {
            OnCloseScreen?.Invoke();
        }

        public static void ExecuteEventOnBuyItem(ItemType itemType, int cost)
        {
            OnBuyItem?.Invoke(itemType, cost);
        }
    }
}