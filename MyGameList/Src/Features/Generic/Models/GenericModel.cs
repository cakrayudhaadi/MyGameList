namespace MyGameList.Src.Features.Generic.Models
{
    public abstract class GenericModel<TId>
    {
        public TId? Id { get; set; }

        protected abstract bool CustomEquals(object other);

        public sealed override bool Equals(object? other)
        {
            if (other == null || GetType() != other.GetType())
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return this.CustomEquals(other);
        }

        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }
}
