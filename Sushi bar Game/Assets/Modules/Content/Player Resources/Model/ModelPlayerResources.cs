using System;
using System.Collections.Generic;
using Modules.Content.Item;
using UnityEngine;

namespace Modules.Content.Player_Resources.Model
{
    public class ModelPlayerResources
    {
        private int _coins = 0;
        private List<ModelItem> _ownedItems = new();

        private int Coins
        {
            get => _coins;

            set
            {
                _coins = value;
                OnCoinValueChanged?.Invoke(_coins);
            }
        }

        public event Action<int> OnCoinValueChanged;

        public void AddCoins(int addValue)
        {
            Coins += addValue;

            Debug.Log(Coins);
        }

        public void PurchaseItem(ModelItem modelItem)
        {
            if (IsEnoughMoney(modelItem.ItemCost) == false) return;

            Coins -= modelItem.ItemCost;

            _ownedItems.Add(modelItem);

            Debug.Log($"Предмет {modelItem.ItemID} куплен! Текущий баланс: {Coins}");
        }
        
        public bool IsEnoughMoney(int coinsAmount)
        {
            return _coins >= coinsAmount;
        }
    }
}