namespace backend.Models.Entities.VerzijaRadaEntitet
{
    public class GotovaVerzijaDto
    {
        public string Status { get; set; }
        public int BrojVerzije { get; set; }
        public string Link { get; set; }
        public bool? VracenNaDoradu { get; set; }
        public string? Napomena { get; set; }
    }
}
