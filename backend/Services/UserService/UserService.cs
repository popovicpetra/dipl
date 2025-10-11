using backend.Database;
using backend.Models.Entities.UserEntitet;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext dbContext;

        public UserService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User?> VratiUsera(LoginUserDto dto)
        {
            return await dbContext.User
                .Include(u => u.TipUser)
                .FirstOrDefaultAsync(u => u.Mejl == dto.Mejl);
        }
    }
}
