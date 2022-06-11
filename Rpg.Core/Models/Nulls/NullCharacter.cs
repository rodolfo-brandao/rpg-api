namespace Rpg.Core.Models.Nulls
{
    public class NullCharacter : Character
    {
        public NullCharacter() : base(playerId: Guid.Empty, name: default, gender: default, mainClass: default, intelligence: default, dexterity: default, strength: default, vitality: default)
        {
            Id = Guid.Empty;
            CreatedAt = default;
            UpdatedAt = default;
            IsDisabled = default;
        }
    }
}
