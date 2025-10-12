using System;
using System.Collections.Generic;
using Modules.Content.UI.Buttons.Events;
using Modules.Content.UI.Screens.Base;
using Modules.Core.Serializeable_Collections.Screens;
using UnityEngine;
using Zenject;

namespace Modules.Core.Managers
{
    public class ManagerScreen : IInitializable, IDisposable
    {
        private readonly Dictionary<ScreenType, BaseScreen> _screens;

        private BaseScreen _currentActiveBaseScreen;

        public ManagerScreen(CollectionScreens collectionScreens)
        {
            _screens = collectionScreens.Screens;
        }

        public void Initialize()
        {
            foreach (var pair in _screens)
            {
                if (pair.Key == ScreenType.Main)
                {
                    _currentActiveBaseScreen = pair.Value;
                    
                    continue;
                }
                
                pair.Value.Close();
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
            _currentActiveBaseScreen.Close();
        }

        private void OpenScreen(ScreenType screenType)
        {
            if (_screens.ContainsKey(screenType) == false) return;

            _currentActiveBaseScreen = _screens[screenType];

            _currentActiveBaseScreen.Open();
        }
    }
}