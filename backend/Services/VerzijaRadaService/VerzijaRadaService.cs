using backend.Database;
using backend.Models.Entities.VerzijaRadaEntitet;

namespace backend.Services.VerzijaRadaService
{
    public class VerzijaRadaService : IVerzijaRadaService
    {
        private readonly AppDbContext dbContext;

        public VerzijaRadaService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DodajVerziju(VerzijaRada verzija)
        {
            await dbContext.AddAsync(verzija);
            await dbContext.SaveChangesAsync();
        }
    }
}
