namespace EyeSoft.Domain.Aggregates
{
    using System;
    using System.Collections.Generic;

    [Serializable]
	public abstract class Entity<TKey> : IEntity where TKey : IComparable<TKey>
	{
		protected Entity()
		{
		}

		protected Entity(TKey key)
		{
			Id = key;
		}

		public virtual TKey Id { get; set; }

		IComparable IEntity.Id
		{
			get => (IComparable)Id;
            set => Id = (TKey)value;
        }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (Entity<TKey>)obj;
			return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}