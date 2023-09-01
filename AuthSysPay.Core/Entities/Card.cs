namespace AuthSysPay.Core
{
    public class Card : BaseEntity
    {
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
