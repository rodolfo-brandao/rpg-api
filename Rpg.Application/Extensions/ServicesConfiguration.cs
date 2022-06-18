using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rpg.Application.Handlers.Auth;
using Rpg.Application.MapperProfiles;
using Rpg.Application.Services;
using Rpg.Core.Contracts.Services;

namespace Rpg.Application.Extensions
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(EntityToResponseProfile));
        }

        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(LoginHandler));
        }

        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            return services.AddScoped<ISecurityService, SecurityService>();
        }
    }
}
