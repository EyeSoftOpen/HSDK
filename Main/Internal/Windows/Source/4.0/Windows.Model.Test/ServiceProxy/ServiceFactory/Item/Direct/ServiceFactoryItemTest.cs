namespace EyeSoft.Windows.Model.Test.Item.Direct
{
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;
	using EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ServiceFactoryItemTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryGetItemWithCompletedActionVerifyWorks()
		{
			CustomerDto customer = null;

			factoryHelper.ServiceFactory
				.Fill(x => x.GetMainCustomer())
				.Completed(x => { customer = x; });

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

			factoryHelper
				.ServiceFactory
				.Fill(x => x.GetMainCustomer())
				.Completed<CustomerViewModel>(x => { model = x; });

			model.Should().Be.EqualTo(Known.CustomerModel.Main);
		}
	}
}