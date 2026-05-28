using Microsoft.Extensions.DependencyInjection;
using UsersMicroService.Core.IRepositories;
using UsersMicroService.Infrastructure.Repositories;

namespace UsersMicroService.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DbFactory.SQLDbFactory>();
            return services;
        }
    }
}
