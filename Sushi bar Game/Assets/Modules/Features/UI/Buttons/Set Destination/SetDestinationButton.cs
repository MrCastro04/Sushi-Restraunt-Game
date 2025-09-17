using System;
using Cysharp.Threading.Tasks;
using Modules.Features.Characters.Base.Code;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Features
{
    public class SetDestinationButton : MonoBehaviour
    {
        [SerializeField] private BaseCharacterMover _characterMover;
        [SerializeField] private Transform _destinationTransform;
        [SerializeField] private float _timeToDestination;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(MoveCharacter);
        }

        private async void MoveCharacter()
        {
           await _characterMover.MoveTo(_destinationTransform, _timeToDestination);
        }
    }
}