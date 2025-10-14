using System;

namespace Modules.Content.Item
{
    public static class EventsItem
    {
        public static event Action OnGetMoreEmployers;
        public static event Action OnGetMoreCustomers;

        public static void ExecuteEventOnGetMoreEmployers() => OnGetMoreEmployers?.Invoke();
        public static void ExecuteEventOnGetMoreCustomers() => OnGetMoreCustomers?.Invoke();
    }
}