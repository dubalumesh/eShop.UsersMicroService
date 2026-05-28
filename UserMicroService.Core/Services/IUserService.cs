
using UsersMicroService.Core.DTOs;

namespace UsersMicroService.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUserResponse>> GetAllUsersAsync();
        Task<string> RegisterUserAsync(RegisterUserRequest request);

    }
}
