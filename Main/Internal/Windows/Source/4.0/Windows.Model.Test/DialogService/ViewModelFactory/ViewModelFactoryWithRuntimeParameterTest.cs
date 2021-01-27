namespace EyeSoft.Windows.Model.Test.DialogService.ViewModelFactory
{
    using Core.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

    [TestClass]
	public class ViewModelFactoryWithRuntimeParameterTest
	{
		[TestMethod]
		public void CreateViewModelUsingViewModelWithRuntimeParameterTest()
		{
			var factory = new ViewModelFactory(null);

			var viewModelType = typeof(DependencyViewModel);
			var runtimeParameterValue = new object();

			var viewModel =
				factory.CreateWithLocatorOrWithReflection(viewModelType, runtimeParameterValue)
					.Convert<DependencyViewModel>();

			viewModel.Should().Not.Be.Null();
			viewModel.Id.Should().Be.SameInstanceAs(runtimeParameterValue);
		}

		private class DependencyViewModel : AutoRegisterViewModel
		{
			public DependencyViewModel(object id)
			{
				Id = id;
			}

			public object Id { get; private set; }
		}

		private class TestDependency
		{
		}
	}
}