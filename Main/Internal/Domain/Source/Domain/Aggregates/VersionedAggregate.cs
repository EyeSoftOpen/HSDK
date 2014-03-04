namespace EyeSoft.Domain
{
	using System;

	[Serializable]
	public abstract class VersionedAggregate : VersionedEntity, IAggregate
	{
		protected VersionedAggregate() : this(Guid.NewGuid())
		{
		}

		protected VersionedAggregate(Guid id)
		{
			Id = id;
		}
	}
}