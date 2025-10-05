using System.Collections.Generic;
using Modules.Content.UI.Screens.Base;
using Modules.Core.Serializeable_Collections.Map_Points;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Modules.Core.Serializeable_Collections.Screens
{
    public class CollectionScreens : SerializedMonoBehaviour
    {
        [OdinSerialize]
        public Dictionary<ScreenType, BaseScreen> Screens { get; private set; }
    }
}