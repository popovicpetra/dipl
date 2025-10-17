using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;
using backend.Services;
using backend.Services.RadService;
using backend.Services.VerzijaRadaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadController : ControllerBase
    {
        private readonly IRadService radService;
       

        public RadController(IRadService radService)
        {
            this.radService = radService;
            
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> DodajRadIPocetnuVerziju([FromForm] AddRadDto dto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (userId == null)
                return Unauthorized("Nedostaje ID korisnika u tokenu.");
            Guid id = Guid.Parse(userId);

            var (rad, verzija) = await radService.DodajRadIPocetnuVerziju(dto, id);

            return Ok(new { Rad = rad, Verzija = verzija});
        }

        [HttpPost("status")]
        public async Task<IActionResult> VratiRadove([FromBody] RadFilterDto filter)
        {
            if (filter == null || filter.IdIzdanje == Guid.Empty || string.IsNullOrEmpty(filter.Status))
                return BadRequest("Neispravan zahtev.");

            var radovi = await radService.VratiRadoveIVerzije(filter);

            return Ok(radovi);
        }


    }
}
