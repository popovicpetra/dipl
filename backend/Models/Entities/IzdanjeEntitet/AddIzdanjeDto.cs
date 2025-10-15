using backend.Models.Entities.RadEntitet;

namespace backend.Models.Entities.IzdanjeEntitet
{
    public class AddIzdanjeDto
    {
        public string Naziv { get; set; }
        public int Volume { get; set; }
        public int Broj { get; set; }
        public string RecAutora { get; set; } 
       


    }
}
