using backend.Models.Entities.IzdanjeEntitet;

namespace backend.Services.IzdanjeService
{
    public interface IIzdanjeService
    {
        public Task<List<Izdanje>> VratiSvaIzdanja();
        public Task DodajIzdanje(Izdanje izdanje);
    }
}
