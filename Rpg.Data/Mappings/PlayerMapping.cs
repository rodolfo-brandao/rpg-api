using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rpg.Core.Models;

namespace Rpg.Data.Mappings
{
    public class PlayerMapping : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player");

            builder.HasKey(player => player.Id);

            builder.Property(player => player.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(player => player.Username).HasColumnType("varchar(50)").IsRequired();
            builder.Property(player => player.Email).HasColumnType("varchar(255)").IsRequired();
            builder.Property(player => player.Password).HasColumnType("char(32)").IsRequired();
            builder.Property(player => player.PasswordSalt).HasColumnType("char(24)").IsRequired();
            builder.Property(player => player.PasswordSalt).HasColumnType("char(24)").IsRequired();
            builder.Property(player => player.Role).HasColumnType("int").IsRequired();
            builder.Property(player => player.CreatedAt).HasColumnType("datetime2").IsRequired();
            builder.Property(player => player.UpdatedAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(player => player.IsDisabled).HasColumnType("bit").IsRequired();

            builder.HasMany(player => player.Characters).WithOne(character => character.Player).HasForeignKey(character => character.PlayerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
