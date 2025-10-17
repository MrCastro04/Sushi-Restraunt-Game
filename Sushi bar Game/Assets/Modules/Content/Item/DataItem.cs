using Modules.Content.FoodCollection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Content.Item
{
    [CreateAssetMenu(menuName = "Data/Item")]
    public class DataItem : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public Sprite ItemCostSprite { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }

        [field: SerializeField]
        [field: ShowIf(nameof(IsFasterGenerateFood))]
        public FoodType FoodType { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public string ItemDescription { get; private set; }
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public int ItemCost { get; private set; }

        private bool IsFasterGenerateFood() => ItemType == ItemType.FasterGenerateFood;
    }
}