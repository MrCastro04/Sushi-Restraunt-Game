using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Modules.Core.Serializeable_Collections.Map_Points
{
    public class CollectionMapPoints : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<string, MapPointInfo> BasePoints { get; private set; }

        public void Init()
        {
            foreach (var keyValuePair in BasePoints)
            {
                keyValuePair.Value.Init(keyValuePair.Key);
            }
        }
    }
}