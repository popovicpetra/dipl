using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.TipRadaEntitet;

namespace backend.Models.Entities.RadEntitet
{
    public class AddRadDto
    {
        public string Naziv { get; set; }
        public int RedniBroj { get; set; }
        public string DOI { get; set; }
        public IFormFile FormFile { get; set; }
        public Guid IdTipRada { get; set; }
        public Guid IdIzdanje { get; set; }
    

    }
}
