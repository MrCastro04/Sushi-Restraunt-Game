using System;
using Modules.Content.Characters.Employer.Code.Events;
using Modules.Content.Characters.Employer.Code.View;
using Modules.Content.Item;
using Modules.Content.Player_Resources.Model;
using Modules.Content.Player_Resources.View;
using Modules.Content.Shop;
using UnityEngine;
using Zenject;

namespace Modules.Content.Player_Resources.View_Model
{
    public class ViewModelPlayerResources : IInitializable, IDisposable  
    {
        private ModelPlayerResources _modelPlayerResources;
        private ViewPlayerResources _viewPlayerResources;

        public event Action<string> OnCoinValueChanged;

        [Inject]
        public ViewModelPlayerResources(ModelPlayerResources modelPlayerResources, ViewPlayerResources viewPlayerResources)
        {
            _modelPlayerResources = modelPlayerResources;
            _viewPlayerResources = viewPlayerResources;
        }

        public void Initialize()
        {
            EventsSubscription();
            
            _viewPlayerResources.Init(this);
            
            _modelPlayerResources.AddCoins(500);
        }

        private void EventsSubscription()
        {
            _modelPlayerResources.OnCoinValueChanged += HandlerOnCoinsValueChange;
            
            EventsEmployer.OnEmployerSellFood += GetProfitFromSell;

            EventsPlayerResources.OnTryBuyItem += TryBuyItem;
        }

        public void Dispose()
        {
            _modelPlayerResources.OnCoinValueChanged -= HandlerOnCoinsValueChange;
            
            EventsEmployer.OnEmployerSellFood -= GetProfitFromSell;
            
            EventsPlayerResources.OnTryBuyItem -= TryBuyItem;
        }

        private void TryBuyItem(ModelItem modelItem)
        {
            if(_modelPlayerResources.IsEnoughMoney(modelItem.ItemCost) == false) 
            {
                Debug.Log("Недостаточно средств!");
                return;
            }
            
            _modelPlayerResources.PurchaseItem(modelItem);
            
            EventsShop.ExecuteEventOnConfirmPurchase(modelItem.ItemID);
        }

        private void GetProfitFromSell(ViewFood viewFood,int profit)
        {
            _modelPlayerResources.AddCoins(profit);
        }

        private void HandlerOnCoinsValueChange(int newCoinValue)
        {
            OnCoinValueChanged?.Invoke(newCoinValue.ToString());
        }
    }
}