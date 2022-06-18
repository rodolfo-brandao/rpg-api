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
        public static IMvcBuilder ConfigureNewtonsoftJson(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<ICharacterRepository, CharacterRepository>()
                .AddScoped<IPlayerRepository, PlayerRepository>();
        }

        public static IServiceCollection ConfigureUnitsOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
