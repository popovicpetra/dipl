using backend.Database;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.IzdanjeService
{
    public class IzdanjeService : IIzdanjeService
    {
        private readonly AppDbContext dbContext;

        public IzdanjeService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Izdanje>> VratiSvaIzdanja()
        {
            return await dbContext.Izdanje.ToListAsync();
        }

        public async Task DodajIzdanje(Izdanje izdanje)
        {
            await dbContext.AddAsync(izdanje);
            await dbContext.SaveChangesAsync();
        }
    }
}
