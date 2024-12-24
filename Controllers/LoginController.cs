using JWTToken.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
            if(user.Username == "admin"  &&  user.Password =="12345")
            {
                _user = new Users { Username = "Manoj Deshwal" };
            }
            return _user;
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],null, expires: DateTime.Now.AddMinutes(1),signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult response = Unauthorized();
            var user_ = AuthenticateUser(user);
            if(user != null)
            {
                var token = GenerateToken(user_);
                response = Ok(new { token = token });

            }
            return response;
        }

    }
}
