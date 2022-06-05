using Rpg.Core.Enums;
using Rpg.Core.Models.Abstract;

namespace Rpg.Core.Models
{
    public class Character : TrackableEntity
    {
        public Guid PlayerId { get; protected set; }
        public string Name { get; protected set; }
        public MainClass MainClass { get; protected set; }
        public ushort Intelligence { get; protected set; }
        public ushort Dexterity { get; protected set; }
        public ushort Strength { get; protected set; }
        public ushort Vitality { get; protected set; }
        public Player Player { get; protected set; }

        public Character(Guid playerId, string name, MainClass mainClass, ushort intelligence, ushort dexterity, ushort strength, ushort vitality)
        {
            Id = Guid.NewGuid();
            PlayerId = playerId;
            Name = name;
            MainClass = mainClass;
            Intelligence = intelligence;
            Dexterity = dexterity;
            Strength = strength;
            Vitality = vitality;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = default;
            IsDisabled = default;
        }

        public Character ChangeName(string name)
        {
            Name = name;
            return this;
        }

        public Character ChangeMainClass(MainClass mainClass)
        {
            MainClass = mainClass;
            return this;
        }

        public Character ChangeIntelligence(ushort intelligence)
        {
            Intelligence = intelligence;
            return this;
        }

        public Character ChangeDexterity(ushort dexterity)
        {
            Dexterity = dexterity;
            return this;
        }

        public Character ChangeStrength(ushort strength)
        {
            Strength = strength;
            return this;
        }

        public Character ChangeVitality(ushort vitality)
        {
            Vitality = vitality;
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
