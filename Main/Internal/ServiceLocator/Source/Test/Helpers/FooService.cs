namespace EyeSoft.ServiceLocator.Test.Helpers
{
	using System;

	public class FooService : IFooService
	{
		public FooService(FooValidator fooValidator, Guid id)
		{
			FooValidator = fooValidator;
			Id = id;
		}

		public FooValidator FooValidator { get; private set; }

		public Guid Id { get; private set; }
	}
}