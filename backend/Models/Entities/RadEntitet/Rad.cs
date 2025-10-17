using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.TipRadaEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;

namespace backend.Models.Entities.RadEntitet
{
    public class Rad
    {
        public Guid Id { get; set; }
        public string Naziv {  get; set; }
        public int RedniBroj { get; set; }
        public string DOI { get; set; }
        public Guid IdTipRada { get; set; }
        [ForeignKey("IdTipRada")]
        [JsonIgnore]
        public TipRada TipRada { get; set; }
        public Guid IdIzdanje { get; set; }
        [ForeignKey("IdIzdanje")]
        [JsonIgnore]
        public Izdanje Izdanje { get; set; }

        [JsonIgnore]
        public virtual ICollection<VerzijaRada> VerzijaRada { get; set; } = new List<VerzijaRada>();
    }
}
