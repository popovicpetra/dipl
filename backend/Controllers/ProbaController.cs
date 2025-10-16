using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbaController : ControllerBase
    {
        private readonly AzureBlobService _blobService;

        // Konstruktor sa dependency injection
        public ProbaController(AzureBlobService blobService)
        {
            _blobService = blobService;
        }
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] FileDto dto)
        {
            var url = await _blobService.UploadAsync(dto.File);
            return Ok(new { FileUrl = url });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            // Preuzmi fajl iz Azure Bloba
            var stream = await _blobService.DownloadAsync(fileName);
            if (stream == null)
                return NotFound("Fajl nije pronađen.");

            // Odredi MIME tip prema ekstenziji
            var mimeType = "application/octet-stream"; // default
            var extension = Path.GetExtension(fileName).ToLower();

            if (extension == ".docx") mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            else if (extension == ".doc") mimeType = "application/msword";
            else if (extension == ".pdf") mimeType = "application/pdf";

            // Vraća fajl sa originalnim imenom za preuzimanje
            return File(stream, mimeType, fileName);
        }

        [HttpDelete("{file}")]
        public async Task<IActionResult> Obrisi(string file)
        {
            bool uspeh = await _blobService.DeleteAsync(file);
            if (!uspeh)
                return NotFound("Ne moze se naci ovaj fajl");

            return Ok(uspeh);
        }
    }
}
