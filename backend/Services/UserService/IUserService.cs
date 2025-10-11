using backend.Models.Entities.UserEntitet;

namespace backend.Services.UserService
{
    public interface IUserService
    {
        public Task<User?> VratiUsera(LoginUserDto dto);
    }
}
