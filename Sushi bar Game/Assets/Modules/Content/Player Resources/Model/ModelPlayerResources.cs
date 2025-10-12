using System;
using UnityEngine;

namespace Modules.Content.Player_Resources.Model
{
    public class ModelPlayerResources
    {
        private int _coins = 0;

        public event Action<int> OnCoinValueChanged;

        public void AddCoins(int addValue)
        {
            _coins += addValue;

            Debug.Log(_coins);
            
            OnCoinValueChanged?.Invoke(_coins);
        }
    }
}