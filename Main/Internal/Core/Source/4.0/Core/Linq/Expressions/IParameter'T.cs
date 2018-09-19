namespace EyeSoft.Linq.Expressions
{
	using System;

	public interface IParameter<out T>
	{
		T Value { get; }

		string Name { get; }

		Type Type { get; }
	}
}