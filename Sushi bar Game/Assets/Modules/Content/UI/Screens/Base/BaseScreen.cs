using System;
using UnityEngine;

namespace Modules.Content.UI.Screens.Base
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public void Open() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }

    public class PlayerResourceScreen : BaseScreen
    {
        [field: SerializeField] public bool IsHideable { get; private set; }
    }
}