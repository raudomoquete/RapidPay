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

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthRequest login)
        {
            if (login == null)
            {
                return BadRequest("Invalid request data");
            }

            var isUsernamePasswordValid = (login.UserName.ToLower() == "testuser@email.com" && login.Password == "test1234");

            if (isUsernamePasswordValid)
            {
                var result = (CreatedResult)CreateToken(login);
                String token = ((dynamic)result.Value).token;
                var loginResponse = new AuthResponse
                {
                    Token = token,
                    responseMsg = new HttpResponseMessage(HttpStatusCode.OK)
                };

                return Ok(new { loginResponse });
            }
            else
            {
                return BadRequest("Username or Password Invalid!");
            }
        }

        [HttpPost]
        private IActionResult CreateToken([FromBody] AuthRequest model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request data");
            }

            var user = new User();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName!),
                new Claim(ClaimTypes.Role, "Aministrator"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Created(string.Empty, results);
        }



    }
}
