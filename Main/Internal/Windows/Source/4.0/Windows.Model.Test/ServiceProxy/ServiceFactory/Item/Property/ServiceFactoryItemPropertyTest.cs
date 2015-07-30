namespace EyeSoft.Windows.Model.Test.Item.Property
{
	using System.Threading;

	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;
	using EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ServiceFactoryItemPropertyTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryLoadAPropertyVerifyWorks()
		{
			var viewModel = new FilledViewModel(factoryHelper.ServiceFactory);

			viewModel.MainCustomer
				.Should().Be.EqualTo(Known.CustomerModel.Main);
		}

		[TestMethod]
		public void ServiceFactoryLoadAPropertyVerifyWorksCompleted()
		{
			var manualResetEvent = new ManualResetEvent(false);

			var viewModel = new ViewModelCompleted(factoryHelper, manualResetEvent);

			manualResetEvent.WaitOne();

			viewModel.MainCustomer.FirstName
				.Should().Be.EqualTo(Known.Customer.Main.FirstName.ToUpper());
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
			public ViewModelCompleted(ServiceFactoryHelper factoryHelper, ManualResetEvent manualResetEvent)
			{
				factoryHelper
					.ServiceFactory
					.Property(this, x => x.MainCustomer)
					.Fill(x => x.GetMainCustomer())
					.Completed(x =>
					{
						manualResetEvent.Set();

						x.FirstName = x.FirstName.ToUpper();
					});
			}
		}

		private abstract class CollectionViewModel : ViewModel
		{
			public CustomerViewModel MainCustomer { get; private set; }
		}
	}
}