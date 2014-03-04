namespace EyeSoft.Domain
{
	using System;

	public interface IValueObject<T> : IEquatable<T> where T : class
	{
	}
}