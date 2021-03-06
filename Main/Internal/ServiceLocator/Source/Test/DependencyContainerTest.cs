﻿namespace EyeSoft.ServiceLocator.Test
{
	using System;
	using CommonServiceLocator;
    using EyeSoft;
    using EyeSoft.ServiceLocator;
	using EyeSoft.ServiceLocator.Test.Helpers;
    using FluentAssertions;

    public abstract class DependencyContainerTest
	{
		protected readonly Guid key = new Guid("c951d82d-3d54-4d8a-9ac7-2d968d2138e2");

		protected readonly FooValidator fooValidator = new FooValidator();

		private IDependencyContainer container;

		private interface INotRegisteredService
		{
		}

		public virtual void Inizialize()
		{
			container = CreateDependencyContainer();

			container.Singleton(fooValidator);
			container.Singleton<IFooService, FooService>();
		}

		public virtual void ResolveAServiceWithRuntimeParameters()
		{
			var customerService = container.GetInstance<IFooService>(id => key);

			customerService.FooValidator
				.Should().NotBeNull().And.Be(fooValidator);

			customerService.Id
				.Should().NotBe(Guid.Empty).And.Be(key);
		}

		public virtual void ResolveANotRegisterServiceExpectedNullInstance()
		{
			Action action = () => container.GetService(typeof(INotRegisteredService));

            action.Should().Throw<ActivationException>();
		}

		protected abstract IDependencyContainer CreateDependencyContainer();
	}
}