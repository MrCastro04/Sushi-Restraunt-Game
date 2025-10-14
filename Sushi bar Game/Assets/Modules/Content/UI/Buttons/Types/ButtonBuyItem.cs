using Modules.Content.UI.Buttons.Base;
using Modules.Content.UI.Buttons.Events;

namespace Modules.Content.UI.Buttons.Types
{
    public class ButtonBuyItem : BaseButton
    {
        private string _itemID;
        
        public void Init(string id)
        {
            _itemID = id;
        }
        
        protected override void OnClick()
        {
            EventsButtonClick.ExecuteEventOnBuyItem(_itemID);
        }
    }
}