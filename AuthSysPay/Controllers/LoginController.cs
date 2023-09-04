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
            var loginResponse = new AuthResponse { };
            AuthRequest loginrequest = new()
            {
                UserName = login.UserName.ToLower(),
                Password = login.Password
            };

            bool isUsernamePasswordValid = false;

            if (login != null)
            {
                isUsernamePasswordValid = loginrequest.Password == "test1234!" ? true : false;
            }

            if (isUsernamePasswordValid)
            {

                string token = CreateToken(loginrequest.UserName);

                loginResponse.Token = token;
                loginResponse.responseMsg = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };

                //return the token
                return Ok(new { loginResponse });
            }
            else
            {
                return BadRequest("Username or Password Invalid!");
            }
        }


        //private TmpToken CreateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName!),
        //        new Claim(ClaimTypes.Role, "Aministrator"),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]!));
        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expiration = DateTime.UtcNow.AddDays(30);
        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: expiration,
        //        signingCredentials: credentials
        //    );

        //    return new TmpToken
        //    {
        //        Token = new JwtSecurityTokenHandler().WriteToken(token),
        //        Expiration = expiration,
        //    };
        //}

        private string CreateToken(string username)
        {

            List<Claim> claims = new()
            {                    
                //list of Claims - only checking username - more claims can be added.
                new Claim("username", Convert.ToString(username)),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Tokens:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
