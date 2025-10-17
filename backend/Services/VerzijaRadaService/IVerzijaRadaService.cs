using backend.Models.Entities.VerzijaRadaEntitet;

namespace backend.Services.VerzijaRadaService
{
    public interface IVerzijaRadaService
    {

        public Task<VerzijaRada> DodajVerziju(AddVerzijaDto dto, Guid idUser);

        public Task<VerzijaRada> IzmeniVerziju(Guid idRad, Status status, int brojVerzije, UpdateVerzijaDto dto);

        public Task<DownloadVerzijaDto> Download(string imeFajla);

    }
}
