using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Contracts.UnitsOfWork;
using Rpg.Data.Repositories;
using Rpg.Data.UnitsOfWork;

namespace Rpg.Data.Extensions
{
    public static class ServicesConfiguration
    {
        public static IMvcBuilder AddNewtonsoftJson(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            });
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<ICharacterRepository, CharacterRepository>()
                .AddScoped<IPlayerRepository, PlayerRepository>();
        }

        public static IServiceCollection AddUnitsOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
