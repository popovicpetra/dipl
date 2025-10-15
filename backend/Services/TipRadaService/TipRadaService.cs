using backend.Database;
using backend.Models.Entities.TipRadaEntitet;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.TipRadaService
{
    public class TipRadaService : ITipRadaService
    {
        private readonly AppDbContext dbContext;

        public TipRadaService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<TipRada>> VratiSveTipoveRada()
        {
           return await dbContext.TipRada.ToListAsync();
        }
    }
}
