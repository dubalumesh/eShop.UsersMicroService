using Mapster;
using UsersMicroService.Core.DTOs;
using UsersMicroService.Core.Entities;

namespace UsersMicroService.Core
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, ApplicationUserResponse>().Map(dest => dest.DisplayName, src => $"{src.FirstName} {src.LastName}");


            // config.NewConfig<ApplicationUserResponse, ApplicationUser>().Map(dest => dest.PasswordHash, src => new byte[0]).Map(dest => dest.PasswordSalt, src => new byte[0]);
        }

    }



}
