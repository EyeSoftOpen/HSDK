namespace EyeSoft.Windows.Model.Test.DialogService.ViewModelFactory
{
    using System.Linq;
    using Core;
    using Core.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using ServiceLocator.Windsor;
    using SharpTestsEx;

    [TestClass]
	public class ViewModelFactoryWithLocatorTest
	{
		[TestMethod]
		public void CreateViewModelUsingViewModelWithDependencyTest()
		{
			var container = new WindsorDependencyContainer();

			var testDependency = new TestDependency();

			container.Singleton(testDependency);
			container.Singleton<DependencyViewModel>();

			var factory = new ViewModelFactory(container);

			var viewModelType = typeof(DependencyViewModel);
			var parameters = Enumerable.Empty<object>().ToArray();

			var viewModel =
				factory.CreateWithLocatorOrWithReflection(viewModelType, parameters)
					.Convert<DependencyViewModel>();

			viewModel.Should().Not.Be.Null();

			viewModel.TestDependency.Should().Not.Be.Null();
			viewModel.TestDependency.Should().Be.SameInstanceAs(testDependency);
		}

		private class DependencyViewModel : AutoRegisterViewModel
		{
			public DependencyViewModel(TestDependency testDependency)
			{
				TestDependency = testDependency;
			}

			public TestDependency TestDependency { get; }
		}

		private class TestDependency
		{
		}
	}
}