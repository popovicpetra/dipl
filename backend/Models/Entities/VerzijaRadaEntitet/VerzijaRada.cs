using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Models.Entities.RadEntitet;

namespace backend.Models.Entities.VerzijaRadaEntitet
{
    public class VerzijaRada
    {
        public Guid IdRad {  get; set; }
        public Status Status { get; set; }
        public int BrojVerzije { get; set; }
        public string Link { get; set; }
        public bool? VracenNaDoradu { get; set; }
        public string? Napomena { get; set; }
        public DateTime Datum {  get; set; }
        public Guid IdUser { get; set; }
        [ForeignKey("IdRad")]
        [JsonIgnore]
        public virtual Rad Rad { get; set; }
    }
}
