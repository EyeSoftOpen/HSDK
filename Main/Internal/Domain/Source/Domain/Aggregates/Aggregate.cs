namespace EyeSoft.Domain.Aggregates
{
    using System;

    [Serializable]
	public abstract class Aggregate : Aggregate<Guid>
	{
		protected Aggregate() : this(Guid.NewGuid())
		{
		}

		protected Aggregate(Guid key) : base(key)
		{
		}
	}
}