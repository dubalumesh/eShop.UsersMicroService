
using FluentValidation;
using Mapster;
using MapsterMapper;
using UsersMicroService.Core.DTOs;
using UsersMicroService.Core.Entities;
using UsersMicroService.Core.Helper;
using UsersMicroService.Core.IRepositories;
using UsersMicroService.Core.Services;

namespace UsersMicroService.Core.ServiceImplementations
{
    internal class UserService(IUserRepository _userRepository, IMapper mapper) : IUserService
    {
        public async Task<IEnumerable<ApplicationUserResponse>> GetAllUsersAsync()
        {

            var users = await _userRepository.GetAllUsersAsync();
            return mapper.Map<IEnumerable<ApplicationUserResponse>>(users);

        }

        public Task<string> RegisterUserAsync(RegisterUserRequest request)
        {
            try
            {
                var user = mapper.Map<ApplicationUser>(request);
                byte[] passwordHash, passwordSalt;
                PasswordHelper.CreatePassword(request.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                var createdUser = _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while registering the user.", ex);
            }
            return Task.FromResult("User registered successfully.");


        }
    }
}
