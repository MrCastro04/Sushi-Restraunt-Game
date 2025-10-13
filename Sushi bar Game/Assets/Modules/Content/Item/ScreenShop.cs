using Modules.Content.UI.Screens.Base;
using UnityEngine;

namespace Modules.Content.Item
{
    public class ScreenShop : BaseScreen
    {
        [SerializeField] private Transform _containerTransform;

        public Transform ContainerTransform => _containerTransform;
    }
}