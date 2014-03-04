namespace EyeSoft.Domain
{
	using System;

	public interface IEntity
	{
		IComparable Id { get; set; }
	}
}