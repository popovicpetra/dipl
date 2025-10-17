using backend.Models.Entities.VerzijaRadaEntitet;

namespace backend.Models.Entities.RadEntitet
{
    public class RadZaFrontDto
    {
        public Guid IdRad { get; set; }
        public string Naziv { get; set; }
        public int RedniBroj { get; set; }
        public string Status { get; set; }
        public string Link { get; set; } 
        public List<GotovaVerzijaDto> VerzijeGotove { get; set; } = new List<GotovaVerzijaDto>();
    }
}
