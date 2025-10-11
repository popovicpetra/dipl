using backend.Database;
using backend.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzdanjeController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public IzdanjeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult VratiSvaIzdanja() { 
            var izdanja =  dbContext.Izdanje.ToList();
            return Ok(izdanja);
        }
        [Authorize]
        [HttpPost]
        public IActionResult dodajIzdanje(AddIzdanjeDto dto) {

            var tip = User.Claims.FirstOrDefault(c => c.Type == "TipUser")?.Value;

            if (tip != "Editor")
                return Forbid(); 

            var izdanje = new Izdanje
            {
                Naziv = dto.Naziv,
                BrojIzdanja = dto.BrojIzdanja,
            };

            dbContext.Add(izdanje);
            dbContext.SaveChanges();

            return Ok(izdanje);
        }
    }
}
