
using System;

namespace Modules.Content.Player_Resources
{
    public static class EventsPlayerResources
    {
        public static Action<int> OnCoinValueChange;

        public static void ExecuteEventOnCoinValueChange(int newValue) => OnCoinValueChange?.Invoke(newValue);
    }
}