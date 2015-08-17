namespace EyeSoft.Windows.Model.Test
{
	using System.Linq;

	using EyeSoft.Extensions;
	using EyeSoft.ServiceLocator.Windsor;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

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