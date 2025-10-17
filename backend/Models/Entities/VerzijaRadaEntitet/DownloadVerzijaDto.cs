namespace backend.Models.Entities.VerzijaRadaEntitet
{
    public class DownloadVerzijaDto
    {
        public Stream Stream { get; set; } 
        public string MimeType { get; set; } 
        public string FileName { get; set; } 
    }
}
