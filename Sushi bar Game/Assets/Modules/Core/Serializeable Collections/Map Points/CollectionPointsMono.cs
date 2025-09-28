using System.Collections.Generic;
using Modules.Content.Map_Points;
using Modules.Features.FoodCollection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    public class CollectionPointsMono : SerializedMonoBehaviour
    {
        [OdinSerialize, InfoBox("B - Buy Point, S - Sell Point, G - Gathering Food Point, CS - Customer Spanw Point")]
        public Dictionary<string, PointMonoInfo> MapPoints { get; private set; }

        public void Init()
        {
            foreach (var keyValuePair in MapPoints)
            {
                keyValuePair.Value.Init(keyValuePair.Key);
            }
        }

        public void AddMapPoint(string name, PointMono pointMono, FoodType foodType)
        {
            
        }
    }
}