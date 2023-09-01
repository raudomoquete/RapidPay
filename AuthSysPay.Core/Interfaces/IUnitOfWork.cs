namespace AuthSysPay.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Card> CardRepository { get; }

        void SaveChages();

        Task SaveChagesAsync();
 
    }
}
