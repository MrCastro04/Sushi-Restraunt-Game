using UnityEngine;

namespace Modules.Content.UI
{
    public class OpenScreenButton : BaseButton
    {
        [SerializeField] private ScreenType _screenType;
        
        protected override void OnClick()
        {
            EventsButtonClick.ExecuteEventOnOpenScreen(_screenType);
        }
    }
}