using System;
using Modules.Content.Characters.Employer.Events;
using Modules.Content.Characters.Employer.View;
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

        #region INITIALIZE

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
            
            _modelPlayerResources.AddCoins(200);
        }
        #endregion

        #region EVENT_SUBSCRIPTION

        private void EventsSubscription()
        {
            _modelPlayerResources.OnCoinValueChanged += HandlerOnCoinValueChange;
            
            EventsEmployer.OnEmployerSellFood += HandlerOnEmployerSellFood;

            EventsPlayerResources.OnTryBuyItem += HandlerOnTryBuyItem;
        }

        public void Dispose()
        {
            _modelPlayerResources.OnCoinValueChanged -= HandlerOnCoinValueChange;
            
            EventsEmployer.OnEmployerSellFood -= HandlerOnEmployerSellFood;
            
            EventsPlayerResources.OnTryBuyItem -= HandlerOnTryBuyItem;
        }

        #endregion

        private void HandlerOnTryBuyItem(ModelItem modelItemToBuy)
        {
            if(_modelPlayerResources.IsEnoughMoney(modelItemToBuy.ItemCost) == false) 
            {
                Debug.Log("Недостаточно средств!");
                return;
            }
       
            _modelPlayerResources.PurchaseItem(modelItemToBuy);
            
            EventsShop.ExecuteEventOnItemPurchased(modelItemToBuy.ItemID);
        }

        private void HandlerOnEmployerSellFood(ViewFood viewFood,int profit)
        {
            _modelPlayerResources.AddCoins(profit);
        }

        private void HandlerOnCoinValueChange(int newCoinValue)
        {
            OnCoinValueChanged?.Invoke(newCoinValue.ToString());
        }
    }
}