namespace Modules.Content.UI
{
    public class CloseScreenButton : BaseButton
    {
        protected override void OnClick()
        {
            EventsButtonClick.ExecuteEventOnCloseScreen();
        }
    }
}