namespace EyeSoft.ServiceLocator.Test.Helpers
{
	using System;

	public interface IFooService
	{
		FooValidator FooValidator { get; }

		Guid Id { get; }
	}
}