using backend.Models.Entities;
using backend.Models.Entities.TipUserEntitet;
using backend.Models.Entities.UserEntitet;
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
    }
}
