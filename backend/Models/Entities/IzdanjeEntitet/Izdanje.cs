using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Entities.UserEntitet;

namespace backend.Models.Entities.IzdanjeEntitet
{
    public class Izdanje
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }
        public int Volume { get; set; }
        public int Broj { get; set; }
        public string RecAutora { get; set; }
        public Guid idUser { get; set; }
        [ForeignKey("idUser")]
        public User User { get; set; }

    }
}
