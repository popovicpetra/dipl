using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.TipRadaEntitet;

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
        public TipRada TipRada { get; set; }
        public Guid IdIzdanje { get; set; }
        [ForeignKey("IdIzdanje")]
        public Izdanje Izdanje { get; set; }
    }
}
