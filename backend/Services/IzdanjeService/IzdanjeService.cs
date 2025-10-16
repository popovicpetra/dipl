using backend.Database;
using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.RadEntitet;
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
            await dbContext.Izdanje.AddAsync(izdanje);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Rad>> VratiSveRadoveZaIzdanje(Guid id)
        {
            return await dbContext.Rad.Where(r=> r.IdIzdanje == id).ToListAsync();
        }

        public async Task<Izdanje> VratiIzdanjePoId(Guid id)
        {
            return await dbContext.Izdanje.FindAsync(id);    
        }
    }
}
