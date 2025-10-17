using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;

namespace backend.Services.RadService
{
    public interface IRadService
    {
        public Task<(Rad, VerzijaRada)> DodajRadIPocetnuVerziju(AddRadDto dto, Guid userId);

        public Task<List<RadZaFrontDto>> VratiRadoveIVerzije(RadFilterDto dto);
    }
}
