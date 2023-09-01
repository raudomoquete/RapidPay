namespace AuthSysPay.Core
{
    public interface ICardService
    {
        Task CreateCard(Card card);

        Task Pay(Card card, decimal amount);

        //Task UpdateBalance(Card card, decimal newBalance);

        Task<decimal> GetBalance(Card card);
    }
}
