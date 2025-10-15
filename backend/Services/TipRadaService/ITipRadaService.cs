using backend.Models.Entities.TipRadaEntitet;

namespace backend.Services.TipRadaService
{
    public interface ITipRadaService
    {
        public Task<List<TipRada>> VratiSveTipoveRada();
    }
}
