namespace EyeSoft.Docs.Testing.Wpf
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
    using EyeSoft.Mapping;
    using EyeSoft.AutoMapper;
    using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Threading;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ViewModelWithFactoryTest
	{
		private interface IFooService : IDisposable
		{
			IEnumerable<FooItem> GetAll();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			WindowsThreadingFactory.SetCurrentThreadScheduler();

			Mapper.Set(() => new AutoMapperMapper());
		}

		[TestMethod]
		public void InitializeACollectionInAViewModelUsingTheServiceFactoryVerifyCountIsCorrectAndCompletedIsExecuted()
		{
			var viewModel = new MainViewModel(new ServiceFactory<IFooService>(() => new FooService()));

			viewModel.FooList.Should().Have.Count.EqualTo(2);
			viewModel.CurrentFooItem.Should().Be.SameInstanceAs(viewModel.FooList.First());
			viewModel.Loaded.Should().Be.True();
		}

		private class MainViewModel : ViewModel
		{
			public MainViewModel(ServiceFactory<IFooService> testServiceFactory)
			{
				Property(() => CurrentFooItem).OnChanged(x => Loaded = true);

				testServiceFactory
					.Collection(this, x => x.FooList)
					.Fill(x => x.GetAll())
					.Completed(x => CurrentFooItem = x.First());
			}

			public bool Loaded { get; private set; }

			public FooItemViewModel CurrentFooItem
			{
				get => GetProperty<FooItemViewModel>();
                private set => SetProperty(value);
            }

			public IEnumerable<FooItemViewModel> FooList { get; set; }
		}

		private class FooService : IFooService
		{
			public IEnumerable<FooItem> GetAll()
			{
				return new[] { new FooItem { Name = "Bill" }, new FooItem { Name = "Joe" } };
			}

			public void Dispose()
			{
			}
		}

		private class FooItem
		{
			public string Name { get; set; }
		}

		private class FooItemViewModel
		{
		}
	}
}