﻿namespace EyeSoft.Windows.Model.Test.ServiceProxy.ServiceFactory.Collection.Property
{
    using System.Collections.Generic;
    using System.Linq;
    using Demo.Configuration.Helpers;
    using Demo.Contract;
    using Demo.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Collections.ObjectModel;
    using FluentAssertions;

    [TestClass]
	public class ServiceFactoryCollectionFieldTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryLoadACollectionFieldVerifyWorks()
		{
			var viewModel = new FilledViewModel(factoryHelper.ServiceFactory);

			viewModel
				.CustomerCollection
				.Select(x => x.Id)
				.Should().BeSameAs(Known.Customer.All.OrderBy(x => x.FirstName).Select(x => x.Id));
		}

		private class FilledViewModel : CollectionViewModel
		{
			public FilledViewModel(ServiceFactory<ICustomerService> customerServiceFactory)
			{
				customerServiceFactory
					.Collection(this, x => x.customerCollection)
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0));
			}
		}

		private abstract class CollectionViewModel : ViewModel
		{
			protected readonly IObservableCollection<CustomerViewModel> customerCollection = null;

			public IEnumerable<CustomerViewModel> CustomerCollection => customerCollection;
        }
	}
}