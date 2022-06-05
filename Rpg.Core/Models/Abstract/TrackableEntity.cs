namespace Rpg.Core.Models.Abstract
{
    public abstract class TrackableEntity : Entity
    {
        public virtual DateTime CreatedAt { get; protected internal set; }
        public virtual DateTime? UpdatedAt { get; protected internal set; }
        public virtual bool IsDisabled { get; protected internal set; }

        public abstract TrackableEntity Disable();
        public abstract TrackableEntity Enable();
        public abstract TrackableEntity UpdatedNow();
    }
}
