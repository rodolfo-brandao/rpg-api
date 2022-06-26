namespace Rpg.Tests.Setup.Helpers
{
    internal static class AutoMapperHelper
    {
        public static IMapper AddMappingProfile(Profile profile)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(profile);
            });

            return new Mapper(mapperConfiguration);
        }
    }
}
