using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rpg.Core.Models;

namespace Rpg.Data.Mappings
{
    public class CharacterMapping : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Character");

            builder.HasKey(character => character.Id);

            builder.Property(character => character.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(character => character.PlayerId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(character => character.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(character => character.Gender).HasColumnType("int").IsRequired();
            builder.Property(character => character.MainClass).HasColumnType("int").IsRequired();
            builder.Property(character => character.Intelligence).HasColumnType("tinyint").IsRequired();
            builder.Property(character => character.Dexterity).HasColumnType("tinyint").IsRequired();
            builder.Property(character => character.Strength).HasColumnType("tinyint").IsRequired();
            builder.Property(character => character.Vitality).HasColumnType("tinyint").IsRequired();
            builder.Property(character => character.CreatedAt).HasColumnType("datetime2").IsRequired();
            builder.Property(character => character.UpdatedAt).HasColumnType("datetime2").IsRequired(false);
            builder.Property(character => character.IsDisabled).HasColumnType("bit").IsRequired();
        }
    }
}
