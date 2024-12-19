using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == null || request.Password == null)
                return StatusCode(500, new { Message = "Usuário ou senha vazios" });

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""));//TODO ADD KEY ON THIS STRING
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "",
                audience: "",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }

    public class LoginRequest// TODO FAZER ENTIDADE SERVICE E TABELA PRA ESSE CARA NA MIGRATION
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
}
