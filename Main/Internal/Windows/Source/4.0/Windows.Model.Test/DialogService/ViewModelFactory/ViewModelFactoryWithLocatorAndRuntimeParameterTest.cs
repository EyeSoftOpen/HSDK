namespace EyeSoft.Windows.Model.Test.DialogService.ViewModelFactory
{
    using Castle.MicroKernel.Registration;
    using Core.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using ServiceLocator.Windsor;
    using SharpTestsEx;

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

			viewModel.Should().Not.Be.Null();

			viewModel.TestDependency.Should().Not.Be.Null();
			viewModel.TestDependency.Should().Be.SameInstanceAs(testDependency);
			viewModel.Id.Should().Be.SameInstanceAs(runtimeParameterValue);
		}

		private class DependencyViewModel : AutoRegisterViewModel
		{
			public DependencyViewModel(TestDependency testDependency, object id)
			{
				TestDependency = testDependency;
				Id = id;
			}

			public TestDependency TestDependency { get; private set; }

			public object Id { get; private set; }
		}

		private class TestDependency
		{
		}
	}
}