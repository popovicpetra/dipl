using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.RadEntitet;

namespace backend.Services.IzdanjeService
{
    public interface IIzdanjeService
    {
        public Task<List<Izdanje>> VratiSvaIzdanja();
        public Task<Izdanje> DodajIzdanje(Guid id, AddIzdanjeDto dto);

        public Task<List<Rad>> VratiSveRadoveZaIzdanje(Guid id);

        public Task<Izdanje> VratiIzdanjePoId(Guid id);

        public Task<bool> IzmeniIzdanje(Guid id, UpdateIzdanjeDto dto);
    }
}
