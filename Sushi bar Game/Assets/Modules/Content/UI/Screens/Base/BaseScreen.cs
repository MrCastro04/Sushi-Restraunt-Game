using System;
using UnityEngine;

namespace Modules.Content.UI.Screens.Base
{
    [RequireComponent(typeof(Canvas))]
    public abstract class BaseScreen : MonoBehaviour
    {
        protected Canvas _canvas;
        
        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Open() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }
}