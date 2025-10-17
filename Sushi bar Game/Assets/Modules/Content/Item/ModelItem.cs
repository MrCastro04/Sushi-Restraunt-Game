using Modules.Content.FoodCollection;
using UnityEditor.Experimental.GraphView;

namespace Modules.Content.Item
{
    public class ModelItem
    {
        private readonly DataItem DataItem;
        private string _dynamicID;

        public int ItemCost => DataItem.ItemCost;
        public string ItemID => _dynamicID;
        public ItemType ItemType => DataItem.ItemType;
        public FoodType FoodType => DataItem.FoodType;

        public ModelItem(DataItem dataItem)
        {
            DataItem = dataItem;
        }

        public void SetNewID(string newID)
        {
          _dynamicID = newID;
        }
    }
}