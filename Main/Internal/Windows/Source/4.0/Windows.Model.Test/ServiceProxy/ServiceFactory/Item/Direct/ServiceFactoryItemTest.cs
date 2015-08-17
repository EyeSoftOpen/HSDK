namespace EyeSoft.Windows.Model.Test.Item.Direct
{
	using System.Threading;

	using EyeSoft.Windows.Model.Demo.Configuration.Helpers;
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ServiceFactoryItemTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryGetItemWithCompletedActionVerifyWorks()
		{
			CustomerDto customer = null;

			var manualResetEvent = new ManualResetEvent(false);

			factoryHelper.ServiceFactory
				.Fill(x => x.GetMainCustomer())
				.Completed(
					x =>
						{
							customer = x;
							manualResetEvent.Set();
						});

			manualResetEvent.WaitOne();

			customer.Should().Be.EqualTo(Known.Customer.Main);

			factoryHelper.ItemLoaded.Should().Be.True();
		}

		[TestMethod]
		public void ServiceFactoryGetItemWithoutCompletedActionVerifyServiceIsNotCalled()
		{
			factoryHelper.ServiceFactory.Fill(x => x.GetMainCustomer());

			factoryHelper.ItemLoaded.Should().Be.False();
		}

		[TestMethod]
		public void ServiceFactoryGetItemWithoutCompletedAncConversioneActionVerifyWorks()
		{
			CustomerViewModel model = null;

			var manuelResetEvent = new ManualResetEvent(false);

			factoryHelper
				.ServiceFactory
				.Fill(x => x.GetMainCustomer())
				.Completed<CustomerViewModel>(
					x =>
					{
						model = x;
						manuelResetEvent.Set();
					});

			manuelResetEvent.WaitOne();

			model.Should().Be.EqualTo(Known.CustomerModel.Main);
		}
	}
}