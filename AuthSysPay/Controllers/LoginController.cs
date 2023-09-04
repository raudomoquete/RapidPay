using AuthSysPay.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace AuthSysPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public LoginController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public async Task <IActionResult> Login([FromBody] AuthRequest login)
        {
            if (login == null)
            {
                return BadRequest("Invalid request data");
            }

            var tmpResult = await _userRepository.LoginAsync(login);

            if (tmpResult.Succeeded)
            {

                var user = await _userRepository.GetUserByEmailAsync(login.UserName);
                var token = CreateToken(user);


                return Ok(new { Token = token });
            }
            else
            {
                return BadRequest("Username or Password Invalid!");
            }
        }

       
        private TmpToken CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role, "Aministrator"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new TmpToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
