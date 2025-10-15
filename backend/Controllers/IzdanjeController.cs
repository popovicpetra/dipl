using System.Security.Claims;
using backend.Database;
using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;
using backend.Services;
using backend.Services.IzdanjeService;
using backend.Services.RadService;
using backend.Services.VerzijaRadaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzdanjeController : ControllerBase
    {
        private readonly IIzdanjeService izdanjeService;
       

        public IzdanjeController(IIzdanjeService izdanjeService)
        {
            this.izdanjeService = izdanjeService;
            
        }

        [Authorize(Roles = "Editor, Lektor, Sekretar, Saradnik")]
        [HttpGet]
        public async Task<IActionResult> VratiSvaIzdanja() { 
            var izdanja = await izdanjeService.VratiSvaIzdanja();
            return Ok(izdanja);
        }

        [Authorize(Roles ="Editor")]
        [HttpPost]
        public async Task<IActionResult> DodajIzdanje(AddIzdanjeDto dto) {

            var userIdStr = User.FindFirstValue("UserId");
            var userId = Guid.Parse(userIdStr);

            var izdanje = new Izdanje
            {
                Naziv = dto.Naziv,
                Volume = dto.Volume,
                Broj = dto.Broj,
                RecAutora = dto.RecAutora,
                idUser = userId
            };
            await izdanjeService.DodajIzdanje(izdanje);
            return Ok(izdanje);
        }
    }
}
