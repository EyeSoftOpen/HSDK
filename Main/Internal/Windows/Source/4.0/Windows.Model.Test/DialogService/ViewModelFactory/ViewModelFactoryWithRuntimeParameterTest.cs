﻿namespace EyeSoft.Windows.Model.Test.DialogService.ViewModelFactory
{
    using EyeSoft.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;
    using ViewModelFactory = Model.ViewModelFactory;

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

			viewModel.Should().NotBeNull();
			viewModel.Id.Should().BeSameAs(runtimeParameterValue);
		}

		private class DependencyViewModel : AutoRegisterViewModel
		{
			public DependencyViewModel(object id)
			{
				Id = id;
			}

			public object Id { get; }
		}

		private class TestDependency
		{
		}
	}
}