using UnityEngine;

namespace Modules.Content.Shop
{
    public class ViewShop : MonoBehaviour
    {
        [SerializeField] private Transform _itemListTransform;
        
        public Transform ItemListTransform => _itemListTransform;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}