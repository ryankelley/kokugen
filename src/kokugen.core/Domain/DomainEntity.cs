

using System;

namespace Kokugen.Core.Domain
{
    [Serializable]
    public abstract class DomainEntity : IEquatable<DomainEntity>
    {
        public virtual Guid Id { get; set; }

        #region IEquatable<DomainEntity> Members

        /// <summary>
        /// Indicates whether the current <see cref="T:CoachesAid.Core.Domain.DomainEntity" /> is equal to another <see cref="T:CoachesAid.Core.Domain.DomainEntity" />.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">A DomainEntity to compare with this object.</param>
        public virtual bool Equals(DomainEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var otherIsTransient = Equals(other.Id, default(Guid));
            var thisIsTransient = Equals(Id, default(Guid));

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(this, other);

            return other.Id.Equals(Id);
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="T:CoachesAid.Core.Domain.DomainEntity" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:CoachesAid.Core.Domain.DomainEntity" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            //if (ReferenceEquals(null, obj)) return false;
            //if (ReferenceEquals(this, obj)) return true;
            //if (obj.GetType() != GetType()) return false;
            //return Equals((DomainEntity) obj);

            var other = obj as DomainEntity;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var otherIsTransient = Equals(other.Id, default(Guid));
            var thisIsTransient = Equals(Id, default(Guid));

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(this, other);

            var isIdEqual = other.Id.Equals(Id);

            if (isIdEqual)
            {
                Type objType = obj.GetType();
                Type myType = GetType();

                if (objType.Name.Contains(myType.Name) && objType.Name.Contains("Proxy"))
                    return true;

                return obj.GetType() == GetType();
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a DomainEntity. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Equals(Id, default(Guid))
                       ? base.GetHashCode()
                       : Id.GetHashCode();
        }

        public static bool operator ==(DomainEntity left, DomainEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DomainEntity left, DomainEntity right)
        {
            return !Equals(left, right);
        }
    }
}