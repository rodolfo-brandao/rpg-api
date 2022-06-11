using Microsoft.EntityFrameworkCore;
using Rpg.Core.Models;
using Rpg.Data.Extensions;
using System.Reflection;

namespace Rpg.Data.Context
{
    public class RpgContext : DbContext
    {
        public RpgContext(DbContextOptions<RpgContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = default;
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())/*.ApplyModelSeeds()*/;
            base.OnModelCreating(modelBuilder);
        }
    }
}
