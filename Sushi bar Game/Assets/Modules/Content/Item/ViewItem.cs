using Modules.Content.UI.Buttons.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Content.Item
{
    public class ViewItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemDescription;
        [SerializeField] private TMP_Text _itemCost;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _itemCostImage;
        [SerializeField] private ButtonBuyItem _buttonBuyItem;

        public void Init(DataItem dataItem , string finalID)
        {
            DisplayView(dataItem);
            
            // ошибка
            
            _buttonBuyItem.Init(finalID);
        }
        
        public void DisplayView(DataItem dataItem)
        {
            _itemName.text = dataItem.ItemName;

            _itemDescription.text = dataItem.ItemDescription;

            _itemCost.text = dataItem.ItemCost.ToString();

            _itemImage.sprite = dataItem.ItemSprite;

            _itemCostImage.sprite = dataItem.ItemCostSprite;
        }
    }
}