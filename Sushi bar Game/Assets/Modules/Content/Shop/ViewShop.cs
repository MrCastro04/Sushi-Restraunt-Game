using System.Collections.Generic;
using Modules.Content.Item;
using UnityEngine;

namespace Modules.Content.Shop
{
    public class ViewShop : MonoBehaviour
    {
        [SerializeField] private Transform _itemListTransform;
        [SerializeField] private ViewItem _viewItemPrefab;
        
        public Transform ItemListTransform => _itemListTransform;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}