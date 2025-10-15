using backend.Models.Entities.TipRadaEntitet;
using backend.Services.TipRadaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipRadaController : ControllerBase
    {
        private readonly ITipRadaService tipRadaService;

        public TipRadaController(ITipRadaService tipRadaService)
        {
            this.tipRadaService = tipRadaService;
        }

        [Authorize(Roles = "Editor")]
        [HttpGet]
        public async Task<IActionResult> VratiSveTipoveRada()
        {
            var tipovi =  await tipRadaService.VratiSveTipoveRada();
            return Ok(tipovi);
        }
    }
}
