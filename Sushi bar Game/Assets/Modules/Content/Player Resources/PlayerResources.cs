namespace Modules.Content.Player_Resources
{
    public class PlayerResources
    {
        private int _coins = 0;

        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;

                EventsPlayerResources.ExecuteEventOnCoinValueChange(_coins);
            }
        }
    }
}