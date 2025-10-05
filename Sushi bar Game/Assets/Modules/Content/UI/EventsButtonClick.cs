using System;

public static class EventsButtonClick
{
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
}