using UnityEngine;

namespace Modules.Content.UI.Screens.Base
{
    public class BaseScreen : MonoBehaviour
    {
        public void Open() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }
}