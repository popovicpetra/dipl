namespace backend.Models.Entities.VerzijaRadaEntitet
{
    public class AddVerzijaDto
    {
        public Status Status { get; set; }
        public int BrojVerzije { get; set; }
        public string Link { get; set; }
        public bool? VracenNaDoradu { get; set; }
        public string? Napomena { get; set; }
        public DateTime Datum { get; set; }
        public Guid IdUser { get; set; }
    }
}
