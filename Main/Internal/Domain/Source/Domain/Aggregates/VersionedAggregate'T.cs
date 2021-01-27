namespace EyeSoft.Domain.Aggregates
{
    using System;

    [Serializable]
	public abstract class VersionedAggregate<T> : VersionedEntity<T>, IAggregate where T : IComparable<T>
	{
	}
}