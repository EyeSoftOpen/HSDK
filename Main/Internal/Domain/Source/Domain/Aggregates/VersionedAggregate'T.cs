namespace EyeSoft.Domain
{
	using System;

	[Serializable]
	public abstract class VersionedAggregate<T> : VersionedEntity<T>, IAggregate where T : IComparable<T>
	{
	}
}