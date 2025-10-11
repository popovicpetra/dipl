using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Database;
using backend.Models.Entities.UserEntitet;
using backend.Services.UserService;
using backend.Token;
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

            return Ok(new { token });

            //return Ok("Uspeh");
        }
    }
}
