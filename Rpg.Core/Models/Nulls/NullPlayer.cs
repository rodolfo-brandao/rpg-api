namespace Rpg.Core.Models.Nulls
{
    public class NullPlayer : Player
    {
        public NullPlayer() : base(username: default, email: default, password: default, passwordSalt: default, role: default)
        {
            Id = Guid.Empty;
            Characters = default;
            CreatedAt = default;
            UpdatedAt = default;
            IsDisabled = default;
        }
    }
}
