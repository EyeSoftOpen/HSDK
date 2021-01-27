namespace EyeSoft.Domain.Aggregates
{
    using System;

    public interface IValueObject<T> : IEquatable<T> where T : class
	{
	}
}