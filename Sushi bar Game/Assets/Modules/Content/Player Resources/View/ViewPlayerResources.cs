using System;
using Modules.Content.Player_Resources.View_Model;
using TMPro;
using UnityEngine;

namespace Modules.Content.Player_Resources.View
{
    public class ViewPlayerResources : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _coinText;

        private ViewModelPlayerResources _viewModelPlayerResources;
        
        public void Init(ViewModelPlayerResources viewModelPlayerResources)
        {
            _viewModelPlayerResources = viewModelPlayerResources;

            _viewModelPlayerResources.OnCoinValueChanged += DisplayCoinText;
        }
        
        public void Dispose()
        {
            _viewModelPlayerResources.OnCoinValueChanged -= DisplayCoinText;
        }
        
        public void DisplayCoinText(string newCoinText)
        {
            _coinText.text = newCoinText;
        }
    }
}