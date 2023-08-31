namespace AuthSysPay.Core
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
