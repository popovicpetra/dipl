 namespace backend.Models.Entities.VerzijaRadaEntitet
{
    public class AddVerzijaDto
    {
        public Guid IdRad { get; set; }
        public Status Status { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
