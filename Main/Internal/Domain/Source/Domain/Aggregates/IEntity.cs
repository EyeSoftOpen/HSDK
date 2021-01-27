namespace EyeSoft.Domain.Aggregates
{
    using System;

    public interface IEntity
	{
		IComparable Id { get; set; }
	}
}