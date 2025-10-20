using UnityEngine;
using UnityEngine.UI;

namespace Modules.Content.UI.Buttons.Base
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private bool _withAnimation = false;
        
        private Button _button;
        protected Animator _animator;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _animator = _withAnimation ? GetComponent<Animator>() : null ;

            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}