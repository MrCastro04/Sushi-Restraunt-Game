
using System;

namespace Modules.New
{
    public static class EventsPlayerResorces
    {
        public static Action<int> OnCoinValueChange;

        public static void ExecuteEventOnCoinValueChange(int newValue) => OnCoinValueChange?.Invoke(newValue);
    }
}