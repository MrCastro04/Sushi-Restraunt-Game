using System.Collections.Generic;
using Modules.Features;
using Modules.Features.Map_Points;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Zenject;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    public class CollectionMapPoints : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<string, PointMono> BasePoints { get; private set; }

        public void Init()
        {
            foreach (var keyValuePair in BasePoints)
            {
                keyValuePair.Value.Init(keyValuePair.Key);
            }
        }
    }
}