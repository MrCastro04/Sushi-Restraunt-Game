using System;
using System.Collections.Generic;
using Modules.Content.UI;
using Modules.Content.UI.Buttons.Events;
using Modules.Content.UI.Screens.Base;
using Modules.Core.Serializeable_Collections.Screens;
using UnityEngine;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerScreen : IInitializable, IDisposable
    {
        private readonly Dictionary<ScreenType, BaseScreen> _baseScreens;
        private readonly Stack<BaseScreen> _screenHistory = new(); 
        
        private BaseScreen _currentActiveScreen;

        public ManagerScreen(CollectionScreens _collectionScreens)
        {
            _baseScreens = _collectionScreens.Screens;
        }

        public void Initialize()
        {
            foreach (var keyValuePair in _baseScreens)
            {
                if (keyValuePair.Key == ScreenType.Main)
                {
                   _currentActiveScreen = keyValuePair.Value;
                    
                   _currentActiveScreen.Open();
                   
                   _screenHistory.Push(_currentActiveScreen);
                   continue;
                }

                keyValuePair.Value.Close();
            }

            EventsButtonClick.OnCloseScreen += CloseScreen;
            EventsButtonClick.OnOpenScreen += OpenScreen;
        }

        public void Dispose()
        {
            EventsButtonClick.OnCloseScreen -= CloseScreen;
            EventsButtonClick.OnOpenScreen -= OpenScreen;
        }

        private void CloseScreen()
        {
            if (_currentActiveScreen != null)
            {
                _currentActiveScreen.Close();
            }

            if (_screenHistory.Count > 0)
            {
                _screenHistory.Pop();
            }

            if (_screenHistory.Count > 0)
            {
                _currentActiveScreen = _screenHistory.Peek();
                
                _currentActiveScreen.Open();
            }
            else
            {
                _currentActiveScreen = null;
            }
        }

        private void OpenScreen(ScreenType screenType)
        {
            if (_baseScreens.ContainsKey(screenType))
            {
                if (_currentActiveScreen != null)
                {
                    _currentActiveScreen.Close();
                }

                _currentActiveScreen = _baseScreens[screenType];
               
               _currentActiveScreen.Open();
               
               _screenHistory.Push(_currentActiveScreen);
            }
            else
            {
                Debug.Log("Нету такого типа скрина");
            }
        }
    }
}