using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;
using backend.Services;
using backend.Services.VerzijaRadaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerzijaRadaController : ControllerBase
    {
        private readonly IVerzijaRadaService verzijaRadaService;
        

        public VerzijaRadaController(IVerzijaRadaService verzijaRadaService) {
            this.verzijaRadaService = verzijaRadaService;
            
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DodajVerzijuRada([FromForm] AddVerzijaDto dto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (userId == null)
                return Unauthorized("Nedostaje ID korisnika u tokenu.");

            Guid id = Guid.Parse(userId);
            var verzija = await verzijaRadaService.DodajVerziju(dto, id);
            return Ok(verzija);

        }

        [Authorize(Roles ="Editor, Sekretar")]
        [HttpPatch("{idRad}/{Status}/{brojVerzije}")]
        public async Task<IActionResult> IzmeniVerziju(Guid idRad, Status status, int brojVerzije, UpdateVerzijaDto dto)
        {
            var verzija = await verzijaRadaService.IzmeniVerziju(idRad, status, brojVerzije, dto);

            if (verzija == null)
                return NotFound();

            return Ok(verzija);
        }

        [Authorize]
        [HttpGet("download/{imeFajla}")]
        public async Task<IActionResult> Download(string imeFajla)
        {
           var odgovor = await verzijaRadaService.Download(imeFajla);
            if(odgovor == null)
            {
                return NotFound();
            }

            return File(odgovor.Stream, odgovor.MimeType, odgovor.FileName);
        }
        
    }
}
