using System;
using System.Collections.Generic;
using Modules.Core.Serializeable_Collections.Screens;
using UnityEngine;
using Zenject;

namespace Modules.Content.UI
{
    public class ManagerScreen : IInitializable, IDisposable
    {
        private readonly Dictionary<ScreenType, BaseScreen> _baseScreens;
        private readonly Stack<BaseScreen> _screenHistory;
        
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
            _currentActiveScreen.Close();

            _screenHistory.Pop();

            _currentActiveScreen = null;
        }

        private void OpenScreen(ScreenType screenType)
        {
            if (_baseScreens.ContainsKey(screenType))
            {
                CloseScreen();
                
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