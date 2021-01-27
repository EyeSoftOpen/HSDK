namespace EyeSoft.Domain.Aggregates
{
    using System;

    [Serializable]
	public abstract class Aggregate<TKey> : Entity<TKey>, IAggregate where TKey : IComparable<TKey>
	{
		protected Aggregate()
		{
		}

		protected Aggregate(TKey key) : base(key)
		{
		}
	}
}