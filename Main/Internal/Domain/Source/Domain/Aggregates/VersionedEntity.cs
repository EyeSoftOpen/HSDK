namespace EyeSoft.Domain
{
	using System;

	[Serializable]
	public abstract class VersionedEntity : VersionedEntity<Guid>
	{
		protected VersionedEntity() : this(Guid.NewGuid())
		{
		}

		protected VersionedEntity(Guid id)
		{
			Id = id;
		}
	}
}