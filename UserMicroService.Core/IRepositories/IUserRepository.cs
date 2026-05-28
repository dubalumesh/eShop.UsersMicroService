using UsersMicroService.Core.Entities;

namespace UsersMicroService.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUserByEmailAsync(string email);
        Task<ApplicationUser?> GetUserByIdAsync(int id);
        Task AddUserAsync(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(int id);
    }
}
