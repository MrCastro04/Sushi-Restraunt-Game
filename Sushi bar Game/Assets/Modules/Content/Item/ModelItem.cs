namespace Modules.Content.Item
{
    public class ModelItem
    {
        private readonly DataItem DataItem;

        public int ItemCost => DataItem.ItemCost;
        public string ItemID => DataItem.ID;
        
        public ModelItem(DataItem dataItem)
        {
            DataItem = dataItem;
        }
        
    }
}