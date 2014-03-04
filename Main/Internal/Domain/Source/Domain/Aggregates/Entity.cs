namespace EyeSoft.Domain
{
	using System;

	[Serializable]
	public abstract class Entity : Entity<Guid>
	{
		protected Entity() : this(Guid.NewGuid())
		{
		}

		protected Entity(Guid id)
		{
			Id = id;
		}
	}
}