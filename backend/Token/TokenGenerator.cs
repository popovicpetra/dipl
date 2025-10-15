using backend.Models.Entities.UserEntitet;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Token
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        // konstruktor - dobija IConfiguration preko Dependency Injection
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(User user)
        {
            //var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            //    HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Key"]
            //));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.TipUser.Naziv)
             };

            //var token = new JwtSecurityToken(
            //    issuer: HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Issuer"],
            //    audience: HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Audience"],
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddHours(2),
            //    signingCredentials: creds
            //);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
