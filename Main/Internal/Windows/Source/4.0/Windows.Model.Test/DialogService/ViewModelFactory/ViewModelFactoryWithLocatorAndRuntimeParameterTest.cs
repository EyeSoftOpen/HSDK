﻿namespace EyeSoft.Windows.Model.Test.DialogService.ViewModelFactory
{
    using Castle.MicroKernel.Registration;
    using EyeSoft.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using ServiceLocator.Windsor;
    using FluentAssertions;

    [TestClass]
	public class ViewModelFactoryWithLocatorAndRuntimeParameterTest
	{
		[TestMethod]
		public void CreateViewModelUsingViewModelWithDependencyAndRuntimeParameterTest()
		{
			var container = new WindsorDependencyContainer();

			var testDependency = new TestDependency();

			container.Container.Register(Component.For<TestDependency>().Instance(testDependency));
			container.Container.Register(Component.For<DependencyViewModel>());

			var factory = new ViewModelFactory(container);

			var viewModelType = typeof(DependencyViewModel);
			var runtimeParameterValue = new object();

			var viewModel =
				factory.CreateWithLocatorOrWithReflection(viewModelType, runtimeParameterValue)
					.Convert<DependencyViewModel>();

			viewModel.Should().NotBeNull();

			viewModel.TestDependency.Should().NotBeNull();
			viewModel.TestDependency.Should().BeSameAs(testDependency);
			viewModel.Id.Should().BeSameAs(runtimeParameterValue);
		}

		private class DependencyViewModel : AutoRegisterViewModel
		{
			public DependencyViewModel(TestDependency testDependency, object id)
			{
				TestDependency = testDependency;
				Id = id;
			}

			public TestDependency TestDependency { get; }

			public object Id { get; }
		}

		private class TestDependency
		{
		}
	}
}