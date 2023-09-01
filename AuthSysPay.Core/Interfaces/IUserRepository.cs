using Microsoft.AspNetCore.Identity;

namespace AuthSysPay.Core
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserByEmailAsync(string email);

        Task CheckRoleAsync(string roleName);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<SignInResult> LoginAsync(AuthRequest model);

        Task LogoutAsync();
    }
}
