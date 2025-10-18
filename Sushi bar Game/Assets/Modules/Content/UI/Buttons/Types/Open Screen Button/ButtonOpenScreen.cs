using Modules.Content.UI.Buttons.Base;
using Modules.Content.UI.Buttons.Events;
using Modules.Content.UI.Screens.Base;
using UnityEngine;

namespace Modules.Content.UI.Buttons.Types.Open_Screen_Button
{
    public class ButtonOpenScreen : BaseButton
    {
        [SerializeField] private ScreenType _screenType;

        protected override void OnClick()
        {
            EventsButtonClick.ExecuteEventOnOpenScreen(_screenType);
        }
    }
}