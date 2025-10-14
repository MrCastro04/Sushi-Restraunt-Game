using System;
using Modules.Content.Shop;
using Modules.Content.UI.Screens.Base;
using UnityEngine;

namespace Modules.Content.UI.Screens.Screen_Shop
{
    public class ScreenShop : BaseScreen
    {
        [SerializeField] private ViewShop _viewShop;
        
        private void OnEnable()
        {
            _viewShop.Show();
        }

        private void OnDisable()
        {
            _viewShop.Hide();
        }
    }
}