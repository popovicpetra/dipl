using backend.Database;
using backend.Models.Entities.VerzijaRadaEntitet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.VerzijaRadaService
{
    public class VerzijaRadaService : IVerzijaRadaService
    {
        private readonly AppDbContext dbContext;
        private readonly AzureBlobService azureBlobService;

        public VerzijaRadaService(AppDbContext dbContext, AzureBlobService azureBlobService)
        {
            this.dbContext = dbContext;
            this.azureBlobService = azureBlobService;
        }

        public async Task<VerzijaRada> DodajVerziju(AddVerzijaDto dto, Guid idUser)
        {
            var link = await azureBlobService.UploadAsync(dto.FormFile);

            var maxBrojVerzije = await dbContext.VerzijaRada.Where(v=> v.IdRad == dto.IdRad && v.Status == dto.Status)
                .MaxAsync(v=> (int?)v.BrojVerzije) ?? 0;

            var verzija = new VerzijaRada
            {
                IdRad = dto.IdRad,
                Status = dto.Status,
                BrojVerzije = maxBrojVerzije + 1,
                Link = link,
                Datum = DateTime.Now,
                IdUser = idUser
            };
            await dbContext.VerzijaRada.AddAsync(verzija);
            await dbContext.SaveChangesAsync();
            return verzija;
        }

        public async Task<DownloadVerzijaDto> Download(string imeFajla)
        {
            return await azureBlobService.DownloadAsync(imeFajla);
        }

        public async Task<VerzijaRada> IzmeniVerziju(Guid idRad, Status status, int brojVerzije, UpdateVerzijaDto dto)
        {
            var verzija = await dbContext.VerzijaRada.FindAsync(idRad,status,brojVerzije);
            if (verzija == null) { 
                return null;
            }

            verzija.VracenNaDoradu = dto.VracenNaDoradu;
            verzija.Napomena = dto.Napomena;
            await dbContext.SaveChangesAsync();
            return verzija;
        }


    }
}
