namespace EyeSoft.Windows.Model.Test.Item.Property
{
	using System.Threading;

	using EyeSoft.Windows.Model.Demo.Configuration.Helpers;
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ServiceFactoryItemPropertyTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryLoadAPropertyVerifyWorks()
		{
		    global::AutoMapper.Mapper.CreateMap<CustomerDto, CustomerViewModel>();

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