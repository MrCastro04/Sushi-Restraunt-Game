using System;
using System.Collections.Generic;
using Modules.Content.Item;
using Modules.Content.Player_Resources;
using Modules.Content.Shop;
using Modules.Content.UI.Buttons.Events;
using Modules.Core.Extensions;
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

            EventsShop.OnItemPurchasedSuccessfully += HandlerOnItemPurchased;
        }

        public void Dispose()
        {
            EventsButtonClick.OnTryBuyItem -= TryBuyItem;

            EventsShop.OnItemPurchasedSuccessfully -= HandlerOnItemPurchased;
        }

        private void CreateItemViews()
        {
            foreach (var data in _dataItems)
            {
                ModelItem modelItem = new(data);

                ViewItem newViewItem = Object.Instantiate(_viewItemPrefab, _viewShop.ItemListTransform);
                
                newViewItem.transform.localPosition = Vector2.zero;

                string finalID = GenerateUniqueKeyForBothDictionaries(data.ID);
                
                modelItem.SetNewID(finalID);
                
                Debug.Log($"{GetType().Name}: Creating {finalID}");
                
                _modelItems.Add(finalID, modelItem);
                _viewItems.Add(finalID, newViewItem);

                newViewItem.Init(data, finalID);
            }
        }

        private string GenerateUniqueKeyForBothDictionaries(string baseID)
        {
            if (_modelItems.ContainsKey(baseID) == false && _viewItems.ContainsKey(baseID) == false)
                return baseID;

            int numberStartIndex = baseID.Length - 1;

            while (numberStartIndex >= 0 && char.IsDigit(baseID[numberStartIndex]))
                numberStartIndex--;

            numberStartIndex++;

            string lettersPart = baseID.Substring(0, numberStartIndex);
            string numberPart = baseID.Substring(numberStartIndex);

            if (int.TryParse(numberPart, out int number) == false)
                number = 0;

            string newKey;
            do
            {
                number++;
                newKey = lettersPart + number;
            } while (_modelItems.ContainsKey(newKey) || _viewItems.ContainsKey(newKey));
            
            return newKey;
        }

        private void TryBuyItem(string itemID)
        {
            if (_modelItems.ContainsKey(itemID) == false) return;
            
            EventsPlayerResources.ExecuteEventOnTryBuyItem(_modelItems[itemID]);
        }

        private void HandlerOnItemPurchased(string itemID)
        {
            if (_viewItems.ContainsKey(itemID) == false)
            {
                return;
            }

            ModelItem item = _modelItems[itemID];

            switch (item.ItemType)
            {
                case ItemType.MoreCustomer:
                    EventsItem.ExecuteEventOnGetMoreCustomers();
                    break;

                case ItemType.MoreEmployer:
                    EventsItem.ExecuteEventOnGetMoreEmployers();
                    break;

                case ItemType.FasterGenerateFood:
                    
                    EventsItem.ExecuteEventOnGetFasterFoodGenerator(item.FoodType);
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