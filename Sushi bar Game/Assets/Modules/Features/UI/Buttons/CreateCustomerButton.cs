using Modules.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Features.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class CreateCustomerButton : MonoBehaviour
    {
        [SerializeField] private Transform _spawnCusmomersTransform;

        [Inject] private FactoryClients _factoryClients;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _factoryClients.CreateAt(_spawnCusmomersTransform.position, _spawnCusmomersTransform.rotation);
        }
    }
}