using backend.Database;
using backend.Models.Entities;
using backend.Services.IzdanjeService;
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

        [HttpGet]
        public async Task<IActionResult> VratiSvaIzdanja() { 
            var izdanja = await izdanjeService.VratiSvaIzdanja();
            return Ok(izdanja);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DodajIzdanje(AddIzdanjeDto dto) {

            var tip = User.Claims.FirstOrDefault(c => c.Type == "TipUser")?.Value;

            if (tip != "Editor")
                return Forbid(); 

            var izdanje = new Izdanje
            {
                Naziv = dto.Naziv,
                BrojIzdanja = dto.BrojIzdanja,
            };

            await izdanjeService.DodajIzdanje(izdanje);

            return Ok(izdanje);
        }
    }
}
