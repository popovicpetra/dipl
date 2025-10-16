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
        private readonly AzureBlobService blobService;
        private readonly IVerzijaRadaService verzijaService;

        public RadController(IRadService radService, AzureBlobService blobService, IVerzijaRadaService verzijaService)
        {
            this.radService = radService;
            this.blobService = blobService;
            this.verzijaService = verzijaService;
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> DodajRadIPocetnuVerziju([FromForm] AddRadDto dto)
        {
            var rad = new Rad
            {
                Naziv = dto.Naziv,
                RedniBroj = dto.RedniBroj,
                DOI = dto.DOI,
                IdIzdanje = dto.IdIzdanje,
                IdTipRada = dto.IdTipRada,
            };

            await radService.DodajRad(rad);

            var url = await blobService.UploadAsync(dto.FormFile);

            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized("Nedostaje ID korisnika u tokenu.");

            var pocetnaVerzija = new VerzijaRada
            {
                IdUser = Guid.Parse(userIdClaim),
                Status = Status.Pocetni,
                BrojVerzije = 1,
                Link = url,
                Datum = DateTime.Now,
                IdRad = rad.Id
            }; 
            await verzijaService.DodajVerziju(pocetnaVerzija);
            return Ok(new { Rad = rad, Verzija = pocetnaVerzija});
        }


    }
}
