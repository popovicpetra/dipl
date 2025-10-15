using backend.Database;
using backend.Models.Entities.RadEntitet;

namespace backend.Services.RadService
{
    public class RadService : IRadService
    {
        private readonly AppDbContext dbContext;

        public RadService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task DodajRad(Rad rad)
        {
            await dbContext.AddAsync(rad);
            await dbContext.SaveChangesAsync();
        }
    }
}
