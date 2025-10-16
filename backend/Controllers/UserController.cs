using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Database;
using backend.Models.Entities.UserEntitet;
using backend.Services.UserService;
using backend.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserService userService;
        private readonly TokenGenerator tokenGenerator;

        public UserController(IUserService userService, TokenGenerator tokenGenerator)
        {
            this.userService = userService;
            this.tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var user = await userService.VratiUsera(dto);

            if (user == null)
            {
                return Unauthorized("Pogrešan mejl");
            }

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Lozinka, user.Lozinka);

            if (!valid)
            {
                return Unauthorized("Pogrešna lozinka.");
            }
            var token = tokenGenerator.GenerateJwtToken(user);

            //OVO SAM DODALA
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(2)
            });

            //return Ok(new { token });

            return Ok("Uspesan login");
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Role = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(-1)
            });


            return Ok("Uspesno ste se izlogovali");
        }

    }
}
