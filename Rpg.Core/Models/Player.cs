using Rpg.Core.Enums;
using Rpg.Core.Models.Abstract;

namespace Rpg.Core.Models
{
    public class Player : TrackableEntity
    {
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string PasswordSalt { get; protected set; }
        public Role Role { get; protected set; }
        public ICollection<Character> Characters { get; protected set; }

        public Player(string username, string email, string password, string passwordSalt, Role role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            Role = role;
            Characters = default;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = default;
            IsDisabled = default;
        }

        public Player ChangeUsername(string username)
        {
            Username = username;
            return this;
        }

        public Player ChangeEmail(string email)
        {
            Email = email;
            return this;
        }

        public Player ChangePassword(string password, string passwordSalt)
        {
            (Password, PasswordSalt) = (password, passwordSalt);
            return this;
        }

        public Player ChangeRole(Role role)
        {
            Role = role;
            return this;
        }

        public override TrackableEntity Disable()
        {
            IsDisabled = true;
            return this;
        }

        public override TrackableEntity Enable()
        {
            IsDisabled = default;
            return this;
        }

        public override TrackableEntity UpdatedNow()
        {
            UpdatedAt = DateTime.UtcNow;
            return this;
        }
    }
}
