using Modules.Core;
using Modules.Core.Factories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Features.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class CreateCustomerButton : MonoBehaviour
    {
        [SerializeField] private Transform _spawnCusmomersTransform;

        [Inject] private FactoryCustomer _factoryCustomer;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _factoryCustomer.CreateItemIn(_spawnCusmomersTransform.position, _spawnCusmomersTransform.rotation);
        }
    }
}