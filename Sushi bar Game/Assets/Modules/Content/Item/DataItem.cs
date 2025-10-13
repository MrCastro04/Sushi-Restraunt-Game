using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Content.Item
{
    [CreateAssetMenu(menuName = "Data/Item")]
    public class DataItem : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public Sprite ItemCostSprite { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public string ItemDesccription { get; private set; }
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public int ItemCost { get; private set; }
    }
}