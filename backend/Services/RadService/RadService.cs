using backend.Database;
using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;
using backend.Services.VerzijaRadaService;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.RadService
{
    public class RadService : IRadService
    {
        private readonly AppDbContext dbContext;
       
        private readonly AzureBlobService azureBlobService;

        public RadService(AppDbContext dbContext, AzureBlobService azureBlobService)
        {
            this.dbContext = dbContext;
            this.azureBlobService = azureBlobService;
        }
        public async Task<(Rad,VerzijaRada)> DodajRadIPocetnuVerziju(AddRadDto dto, Guid userId)
        {
            var rad = new Rad
            {
                Naziv = dto.Naziv,
                RedniBroj = dto.RedniBroj,
                DOI = dto.DOI,
                IdIzdanje = dto.IdIzdanje,
                IdTipRada = dto.IdTipRada,
            };

            await dbContext.Rad.AddAsync(rad);
            
            var url = await azureBlobService.UploadAsync(dto.FormFile);

            var maxBrojVerzije = await dbContext.VerzijaRada
                                .Where(v => v.IdRad == rad.Id && v.Status == Status.Pocetni)
                                .MaxAsync(v => (int?)v.BrojVerzije) ?? 0;

            var pocetnaVerzija = new VerzijaRada
            {
                IdUser = userId,
                Status = Status.Pocetni,
                BrojVerzije = maxBrojVerzije + 1,
                Link = url,
                Datum = DateTime.Now,
                IdRad = rad.Id
            };
            await dbContext.VerzijaRada.AddAsync(pocetnaVerzija);
            await dbContext.SaveChangesAsync();
            return (rad, pocetnaVerzija);
        }

        public async Task<List<RadZaFrontDto>> VratiRadoveIVerzije(RadFilterDto dto)
        {
            var statusMap = dto.Status.ToLower() switch
            {
                "pocetni" => new List<Status> { Status.Pocetni },
                "lektura" => new List<Status> { Status.ZaLekturu, Status.GotovaLektura },
                "priprema" => new List<Status> { Status.ZaPripremu, Status.GotovaPriprema },
                _ => new List<Status>()
            };

            if (!statusMap.Any())
                return null;
            var radovi = await dbContext.Rad
                .Where(r => r.IdIzdanje == dto.IdIzdanje)
                .Include(r => r.VerzijaRada)
                .ToListAsync();

            var rezultat = new List<RadZaFrontDto>();

            foreach (var rad in radovi)
            {
                // osnovna verzija je prva u listi
                var osnovnaStatus = statusMap.First();
                var osnovna = rad.VerzijaRada.FirstOrDefault(v => v.Status == osnovnaStatus);

                // sve ostale verzije iz iste grupe koje nisu osnovna
                var gotove = rad.VerzijaRada
                    .Where(v => statusMap.Contains(v.Status) && v != osnovna)
                    .OrderByDescending(v => v.BrojVerzije)
                    .Select(v => new GotovaVerzijaDto
                    {
                        Status = v.Status.ToString(),
                        BrojVerzije = v.BrojVerzije,
                        Link = Path.GetFileName(v.Link),
                        VracenNaDoradu = v.VracenNaDoradu,
                        Napomena = v.Napomena,

                    }).ToList();

                var dto2 = new RadZaFrontDto
                {
                    IdRad = rad.Id,
                    Naziv = rad.Naziv,
                    RedniBroj = rad.RedniBroj,
                    Status = osnovna?.Status.ToString(),
                    Link = osnovna != null ? Path.GetFileName(osnovna.Link) : null,
                    VerzijeGotove = gotove
                };

                rezultat.Add(dto2);
            }

            return rezultat;
        }
    }
    
}
