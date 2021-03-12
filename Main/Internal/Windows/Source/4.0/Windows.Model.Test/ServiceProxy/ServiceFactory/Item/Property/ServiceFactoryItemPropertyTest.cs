namespace EyeSoft.Windows.Model.Test.ServiceProxy.ServiceFactory.Item.Property
{
    using System.Threading;
    using Demo.Configuration.Helpers;
    using Demo.Contract;
    using Demo.ViewModels;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

    [TestClass]
	public class ServiceFactoryItemPropertyTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryLoadAPropertyVerifyWorks()
		{
			var viewModel = new FilledViewModel(factoryHelper.ServiceFactory);

			viewModel.MainCustomer
				.Should().Be(Known.CustomerModel.Main);
		}

		[TestMethod]
		public void ServiceFactoryLoadAPropertyVerifyWorksCompleted()
		{
			var manualResetEvent = new ManualResetEvent(false);

			var viewModel = new ViewModelCompleted(factoryHelper, manualResetEvent);

			manualResetEvent.WaitOne();

			viewModel.MainCustomer.FirstName
				.Should().Be(Known.Customer.Main.FirstName.ToUpper());
		}

		private class FilledViewModel : CollectionViewModel
		{
			public FilledViewModel(ServiceFactory<ICustomerService> customerServiceFactory)
			{
				customerServiceFactory
					.Property(this, x => x.MainCustomer)
					.Fill(x => x.GetMainCustomer());
			}
		}

		private class ViewModelCompleted : CollectionViewModel
		{
			public ViewModelCompleted(ServiceFactoryHelper factoryHelper, EventWaitHandle manualResetEvent)
			{
				factoryHelper
					.ServiceFactory
					.Property(this, x => x.MainCustomer)
					.Fill(x => x.GetMainCustomer())
					.Completed(x =>
					{
						x.FirstName = x.FirstName.ToUpper();

						manualResetEvent.Set();
					});
			}
		}

		private abstract class CollectionViewModel : ViewModel
		{
			public CustomerViewModel MainCustomer { get; private set; }
		}
	}
}