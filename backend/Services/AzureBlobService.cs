using Azure.Storage.Blobs;
using backend.Models.Entities.VerzijaRadaEntitet;
namespace backend.Services
{
    public class AzureBlobService
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobService(IConfiguration config)
        {
            var connectionString = config["AzureBlobStorage:ConnectionString"];
            var containerName = config["AzureBlobStorage:ContainerName"];

            _containerClient = new BlobContainerClient(connectionString, containerName);
            _containerClient.CreateIfNotExists(); // kreira container ako ne postoji
        }

        // Upload fajla
        public async Task<string> UploadAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = _containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            // vraća URL fajla u Blob-u
            return blobClient.Uri.ToString();
        }

        // Download fajla
        public async Task<DownloadVerzijaDto?> DownloadAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            
            if (!await blobClient.ExistsAsync())
                return null;

            var odgovor = await blobClient.DownloadAsync();
            var ekstenzija = Path.GetExtension(fileName).ToLowerInvariant();
            var mime = ekstenzija switch
            {
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".doc" => "application/msword",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            };

            return new DownloadVerzijaDto
            {
                Stream = odgovor.Value.Content,
                MimeType = mime,
                FileName = fileName
            };
        }

        // Brisanje fajla
        public async Task<bool> DeleteAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            return await blobClient.DeleteIfExistsAsync();
        }
    }
}

