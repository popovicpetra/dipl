using backend.Models.Entities.IzdanjeEntitet;
using backend.Models.Entities.RadEntitet;
using backend.Models.Entities.TipRadaEntitet;
using backend.Models.Entities.TipUserEntitet;
using backend.Models.Entities.UserEntitet;
using backend.Models.Entities.VerzijaRadaEntitet;
using Microsoft.EntityFrameworkCore;

namespace backend.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Izdanje> Izdanje { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TipUser> TipUser { get; set; }

        public DbSet<TipRada> TipRada { get; set; }
        public DbSet<Rad> Rad { get; set; }
        public DbSet<VerzijaRada> VerzijaRada {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VerzijaRada>()
                .HasKey(v => new { v.IdRad, v.Status, v.BrojVerzije });

            base.OnModelCreating(modelBuilder);
        }

    }
}
