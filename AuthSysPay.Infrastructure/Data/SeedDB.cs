using AuthSysPay.Core;

namespace AuthSysPay.Infrastructure
{
    public class SeedDB
    {
        private readonly AuthSysPayContext _context;
        private readonly IUserRepository _userRepository;

        public SeedDB(
            AuthSysPayContext context,
            IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("TestUser", "AnyLastName", "testuser@email.com", "Administrator");
        }

        private async Task CheckRolesAsync()
        {
            await _userRepository.CheckRoleAsync("Administrator");
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string role)
        {
            var tmpUser = await _userRepository.GetUserByEmailAsync(email);

            if (tmpUser == null)
            {
                tmpUser = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                };

                await _userRepository.AddUserAsync(tmpUser, "test1234!");
                await _userRepository.AddUserToRoleAsync(tmpUser, role);
                //var token = await _userRepository.GenerateEmailConfirmationTokenAsync(tmpUser);
                //await _userRepository.ConfirmEmailAsync(tmpUser, token);
            }

            return tmpUser;
        }

    }
}
