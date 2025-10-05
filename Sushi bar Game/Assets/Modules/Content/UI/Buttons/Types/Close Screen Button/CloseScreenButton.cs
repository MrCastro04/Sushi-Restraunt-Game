using Modules.Content.UI.Buttons.Base;
using Modules.Content.UI.Buttons.Events;

namespace Modules.Content.UI.Buttons.Types.Close_Screen_Button
{
    public class CloseScreenButton : BaseButton
    {
        protected override void OnClick()
        {
            EventsButtonClick.ExecuteEventOnCloseScreen();
        }
    }
}