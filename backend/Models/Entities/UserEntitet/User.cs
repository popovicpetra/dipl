using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Entities.TipUserEntitet;

namespace backend.Models.Entities.UserEntitet
{
    public class User
    {
        public Guid Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Mejl { get; set; }
        public string Lozinka { get; set; }
        public Guid IdTip { get; set; }
        [ForeignKey("IdTip")]
        public TipUser TipUser { get; set; }
    }
}
