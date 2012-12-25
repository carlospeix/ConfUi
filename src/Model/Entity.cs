using System;

namespace Centros.Model
{
    public abstract class Entity : AbstractEntity<Guid>
    {
        private Guid _id;
        public override Guid Id
        {
            get { return _id; }
            protected set { _id = value; }
        }
    }

    public abstract class AbstractEntity<TId> : IEquatable<AbstractEntity<TId>>
    {
        public abstract TId Id { get; protected set; }

        public virtual bool Equals(AbstractEntity<TId> other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            var otherType = other.GetUnproxiedType();
            var thisType = GetUnproxiedType();

            if (!otherType.IsAssignableFrom(thisType) && !thisType.IsAssignableFrom(otherType))
                return false;

            bool otherIsTransient = other.IsTransient();
            bool thisIsTransient = IsTransient();

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return Equals(Id, other.Id);
        }

        protected bool IsTransient()
        {
            return Equals(Id, default(TId));
        }

        public override bool Equals(object obj)
        {
            var that = obj as AbstractEntity<TId>;

            return Equals(that);
        }

        private int? _requestedHashCode;
        public override int GetHashCode()
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = IsTransient() ? base.GetHashCode() : Id.GetHashCode();

            return _requestedHashCode.Value;
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

    }
}