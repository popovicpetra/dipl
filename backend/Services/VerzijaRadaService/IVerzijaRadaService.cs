using backend.Models.Entities.VerzijaRadaEntitet;

namespace backend.Services.VerzijaRadaService
{
    public interface IVerzijaRadaService
    {

        public Task DodajVerziju(VerzijaRada verzija);
    }
}
