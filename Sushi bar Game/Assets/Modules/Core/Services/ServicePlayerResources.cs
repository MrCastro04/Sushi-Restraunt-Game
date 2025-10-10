using Modules.Content.Player_Resources;

namespace Modules.Core.Services
{
    public class ServicePlayerResources
    {
        private readonly PlayerResources _playerResources;

        public ServicePlayerResources(PlayerResources playerResources)
        {
            _playerResources = playerResources;
        }

        public void AddResource(PlayerResourceType playerResourceType, int countToAdd)
        {
            if (countToAdd <= 0) return;

            switch (playerResourceType)
            {
                case PlayerResourceType.Coin:
                    _playerResources.Coins += countToAdd;
                    break;
            }
        }
    }
}