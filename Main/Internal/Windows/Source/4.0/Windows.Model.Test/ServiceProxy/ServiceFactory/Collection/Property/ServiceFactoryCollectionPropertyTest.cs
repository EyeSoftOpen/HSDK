namespace EyeSoft.Windows.Model.Test.ServiceProxy.ServiceFactory.Collection.Property
{
    using System;
    using System.Linq;
    using System.Threading;
    using Demo.Configuration.Helpers;
    using Demo.Contract;
    using Demo.ViewModels;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Collections.ObjectModel;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

    [TestClass]
	public class ServiceFactoryCollectionPropertyTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryLoadACollectionVerifyWorks()
		{
			var viewModel = new FilledViewModel(factoryHelper.ServiceFactory);

			viewModel
				.CustomerCollection
				.Select(x => x.Id)
				.Should().BeSameAs(Known.Customer.All.OrderBy(x => x.FirstName).Select(x => x.Id));
		}

		[TestMethod]
		public void VerifyPreAssignedCollectionIsClearedAndFilledButNotReInitialized()
		{
			var collection = new ConcurrentObservableCollection<CustomerViewModel>();

			var viewModel = new PreAssignedCollectionViewModel(collection, factoryHelper.ServiceFactory);


			viewModel.CustomerCollection.Should().BeSameAs(collection);
			viewModel.CustomerCollection.Should().HaveCount(3);
		}

		[TestMethod]
		public void ServiceFactoryLoadACollectionVerifyWorksSorted()
		{
			var viewModel = new ViewModelSorted(factoryHelper.ServiceFactory);

			var listCollectionView = viewModel.CustomerCollection.ListView();

			listCollectionView.Should().NotBeNull();
		}

		[TestMethod]
		public void ServiceFactoryLoadACollectionVerifyWorksCompleted()
		{
			var manualResetEvent = new ManualResetEvent(false);

			var viewModel = new ViewModelCompleted(factoryHelper, manualResetEvent);

			var all = Known.Customer.All.Select(x => x.Id);

			manualResetEvent.WaitOne();

			viewModel
				.CustomerCollection
				.Select(x => x.Id)
				.Should().BeSameAs(all);

			viewModel.Completed.Should().Be(all.First());
		}

		private class PreAssignedCollectionViewModel : CollectionViewModel
		{
			public PreAssignedCollectionViewModel(
				IObservableCollection<CustomerViewModel> collection,
				ServiceFactory<ICustomerService> customerServiceFactory)
			{
				CustomerCollection = collection;

				customerServiceFactory
					.Collection(this, x => x.CustomerCollection)
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0));
			}
		}

		private class FilledViewModel : CollectionViewModel
		{
			public FilledViewModel(ServiceFactory<ICustomerService> customerServiceFactory)
			{
				customerServiceFactory
					.Collection(this, x => x.CustomerCollection)
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0));
			}
		}

		private class ViewModelSorted : CollectionViewModel
		{
			public ViewModelSorted(ServiceFactory<ICustomerService> customerServiceFactory)
			{
				customerServiceFactory
					.Collection(this, x => CustomerCollection)
					.Sort(x => x.LastName)
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0));
			}
		}

		private class ViewModelCompleted : CollectionViewModel
		{
			public ViewModelCompleted(ServiceFactoryHelper factoryHelper, ManualResetEvent manualResetEvent)
			{
				factoryHelper
					.ServiceFactory
					.Collection(this, x => x.CustomerCollection)
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0))
					.Completed(x =>
					{
						Completed = x.First().Id;

						manualResetEvent.Set();
					});
			}

			public Guid Completed { get; private set; }
		}

		private abstract class CollectionViewModel : ViewModel
		{
			public IObservableCollection<CustomerViewModel> CustomerCollection
			{
				get; protected set;
			}
		}
	}
}