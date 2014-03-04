namespace EyeSoft.Windows.Model.Test
{
	using System;
	using System.Linq;

	using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;
	using EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

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
				.Should().Have.SameSequenceAs(Known.Customer.All.OrderBy(x => x.FirstName).Select(x => x.Id));
		}

		[TestMethod]
		public void VerifyPreAssignedCollectionIsClearedAndFilledButNotReInitialized()
		{
			var collection = new ConcurrentObservableCollection<CustomerViewModel>();

			var viewModel = new PreAssignedCollectionViewModel(collection, factoryHelper.ServiceFactory);


			viewModel.CustomerCollection.Should().Be.SameInstanceAs(collection);
			viewModel.CustomerCollection.Should().Have.Count.EqualTo(3);
		}

		[TestMethod]
		public void ServiceFactoryLoadACollectionVerifyWorksSorted()
		{
			var viewModel = new ViewModelSorted(factoryHelper.ServiceFactory);

			var listCollectionView = viewModel.CustomerCollection.ListView();

			listCollectionView.Should().Not.Be.Null();
		}

		[TestMethod]
		public void ServiceFactoryLoadACollectionVerifyWorksCompleted()
		{
			var viewModel = new ViewModelCompleted(factoryHelper);

			var all = Known.Customer.All.Select(x => x.Id);

			viewModel
				.CustomerCollection
				.Select(x => x.Id)
				.Should().Have.SameSequenceAs(all);

			viewModel.Completed.Should().Be.EqualTo(all.First());
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
			public ViewModelCompleted(ServiceFactoryHelper factoryHelper)
			{
				factoryHelper
					.ServiceFactory
					.Collection(this, x => x.CustomerCollection)
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0))
					.Completed(x =>
					{
						Completed = x.First().Id;
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