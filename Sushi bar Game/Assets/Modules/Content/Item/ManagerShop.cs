using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.Content.Item
{
    public class ManagerShop
    {
        private readonly ScreenShop _screenShop;
        private readonly List<DataItem> _dataItems;
        private readonly ViewItem _viewItemPrefab;

        [Inject]
        public ManagerShop(List<DataItem> dataItems, ViewItem viewItemPrefab, ScreenShop screenShop)
        {
            _screenShop = screenShop;

            _viewItemPrefab = viewItemPrefab;

            _dataItems = dataItems;

            foreach (var data in _dataItems)
            {
                AddNewViewItem(data);
            }
        }

        public void AddNewViewItem(DataItem dataItem)
        {
            ViewItem newViewItem = GameObject
                .Instantiate(_viewItemPrefab, _screenShop.ContainerTransform);

            newViewItem.transform.localPosition = Vector2.zero;
            
            newViewItem.DisplayView(dataItem);
        }
    }
}