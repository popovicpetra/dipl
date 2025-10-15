using backend.Models.Entities.RadEntitet;

namespace backend.Services.RadService
{
    public interface IRadService
    {
        public Task DodajRad(Rad rad);
    }
}
