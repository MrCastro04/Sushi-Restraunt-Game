namespace Modules.New
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

                EventsPlayerResorces.ExecuteEventOnCoinValueChange(_coins);
            }
        }
    }
}