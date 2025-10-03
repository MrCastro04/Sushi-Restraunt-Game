using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

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
    }
}