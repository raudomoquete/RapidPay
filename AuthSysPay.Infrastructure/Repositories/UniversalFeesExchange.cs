namespace AuthSysPay.Infrastructure
{
    public class UniversalFeesExchange
    {
        private static readonly Lazy<UniversalFeesExchange>
            _instance = new Lazy<UniversalFeesExchange>(() => new  UniversalFeesExchange());
        private decimal _currentFee;
        private readonly Random _random;

        private UniversalFeesExchange()
        {
            _currentFee = 1;
            _random = new Random();
        }

        public static UniversalFeesExchange Instance => _instance.Value;

        public decimal GetCurrentFee()
        {
            return _currentFee;
        }

        public void UpdateFee()
        {
            var tmpRandom = (decimal)_random.NextDouble() * 2;
            _currentFee *= tmpRandom;
        }
    }
}
