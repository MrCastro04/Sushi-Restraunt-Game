using System;
using System.Collections.Generic;
using Modules.Content.Item;
using Modules.Content.Player_Resources;
using Modules.Content.Shop;
using Modules.Content.UI.Buttons.Events;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Modules.Core.Managers
{
    public class ManagerShop : IInitializable, IDisposable
    {
        private readonly Dictionary<string, ModelItem> _modelItems = new();
        private readonly Dictionary<string, ViewItem> _viewItems = new();
        private readonly List<DataItem> _dataItems;
        private readonly ViewShop _viewShop;
        private readonly ViewItem _viewItemPrefab;

        [Inject]
        public ManagerShop(List<DataItem> dataItems, ViewItem viewItemPrefab, ViewShop viewShop)
        {
            _viewShop = viewShop;

            _dataItems = dataItems;

            _viewItemPrefab = viewItemPrefab;
        }

        public void Initialize()
        {
            CreateItemViews();

            EventsButtonClick.OnTryBuyItem += TryBuyItem;

            EventsShop.OnItemPurchasedSuccessfully += RemoveTargetItemView;
        }

        public void Dispose()
        {
            EventsButtonClick.OnTryBuyItem -= TryBuyItem;

            EventsShop.OnItemPurchasedSuccessfully -= RemoveTargetItemView;
        }

        private void CreateItemViews()
        {
            foreach (var data in _dataItems)
            {
                ModelItem modelItem = new(data);

                ViewItem newViewItem = Object.Instantiate(_viewItemPrefab, _viewShop.ItemListTransform);

                newViewItem.transform.localPosition = Vector2.zero;

                newViewItem.Init(data);

                _modelItems.Add(data.ID, modelItem);
                _viewItems.Add(data.ID, newViewItem);
            }
        }

        private void TryBuyItem(string itemID)
        {
            if (_modelItems.ContainsKey(itemID) == false) return;

            ModelItem itemToBuy = _modelItems[itemID];

            EventsPlayerResources.ExecuteEventOnTryBuyItem(itemToBuy);
        }

        private void RemoveTargetItemView(string itemID)
        {
            if (_viewItems.ContainsKey(itemID) == false) return;
            
            switch (_modelItems[itemID].ItemType)
            {
                case ItemType.MoreCustomer :
                    EventsItem.ExecuteEventOnGetMoreCustomers();
                    break;
                
                case ItemType.MoreEmployer:
                    EventsItem.ExecuteEventOnGetMoreEmployers();
                    break;
                
                default:
                    Debug.Log($"Такой способности не существует. Добавте ее через | {this} | ");
                    return;
                
            }

            Object.Destroy(_viewItems[itemID].gameObject);

            _viewItems.Remove(itemID);

            _modelItems.Remove(itemID);
        }
    }
}