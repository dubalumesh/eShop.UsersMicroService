

using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersMicroService.Core.ServiceImplementations;
using UsersMicroService.Core.Services;
using FluentValidation;
using UsersMicroService.Core.Validators;

namespace UsersMicroService.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // 1. Get the global config instance
            var config = TypeAdapterConfig.GlobalSettings;
            // 2. Scan the assembly to find all classes implementing IRegister
            config.Scan(Assembly.GetExecutingAssembly());
            // 3. Register the Config and Mapper instances into DI
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            // 4. Register FluentValidation validators
            services.AddValidatorsFromAssemblyContaining<RegisterUserRequestValidator>();
            // 5. Register other services
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
