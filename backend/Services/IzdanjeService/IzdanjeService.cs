using backend.Database;
using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.UserEntitet;
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

        public async Task<Izdanje> DodajIzdanje(Guid id,AddIzdanjeDto dto)
        {
            var izdanje = new Izdanje
            {
                Naziv = dto.Naziv,
                Volume = dto.Volume,
                Broj = dto.Broj,
                RecAutora = dto.RecAutora,
                Izdato = dto.Izdato,
                idUser = id
            };
            await dbContext.Izdanje.AddAsync(izdanje);
            await dbContext.SaveChangesAsync();
            return izdanje;
        }

        public async Task<List<Rad>> VratiSveRadoveZaIzdanje(Guid id)
        {
            return await dbContext.Rad.Where(r=> r.IdIzdanje == id).ToListAsync();
        }

        public async Task<Izdanje> VratiIzdanjePoId(Guid id)
        {
            return await dbContext.Izdanje.FindAsync(id);    
        }

        public async Task<bool> IzmeniIzdanje(Guid id,UpdateIzdanjeDto dto)
        {
            var izdanje = await dbContext.Izdanje.FindAsync(id);
            if (izdanje == null)
                return false;
            izdanje.Izdato = dto.Izdato;
            await dbContext.SaveChangesAsync();
            return true;

        }
    }
}
