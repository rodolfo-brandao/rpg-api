using Microsoft.EntityFrameworkCore;
using Rpg.Core.Models;
using Rpg.Core.Models.Seeds;

namespace Rpg.Data.Extensions
{
    internal static class ModelSeed
    {
        internal static ModelBuilder ApplyModelSeeds(this ModelBuilder builder)
        {
            builder.Entity<Player>().HasData(PlayerSeed.Models);
            return builder;
        }
    }
}
