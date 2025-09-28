using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    public class CollectionMapPoints : SerializedMonoBehaviour
    {
        [OdinSerialize, InfoBox("B - Buy Point, S - Sell Point, G - Gathering Food Point, CS - Customer Spanw Point")]
        public Dictionary<string, MapPointInfo> MapPoints { get; private set; }

        public void Init()
        {
            foreach (var keyValuePair in MapPoints)
            {
                keyValuePair.Value.Init(keyValuePair.Key);
            }
        }
    }
}