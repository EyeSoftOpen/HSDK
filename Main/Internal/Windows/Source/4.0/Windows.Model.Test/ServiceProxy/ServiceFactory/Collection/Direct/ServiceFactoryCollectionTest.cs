namespace EyeSoft.Windows.Model.Test
{
	using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Demo.ViewModels;
	using EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ServiceFactoryCollectionTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryGetCollectionWithCompletedActionVerifyWorks()
		{
			IObservableCollection<CustomerViewModel> collection = null;

			factoryHelper
				.ServiceFactory
				.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0))
				.Completed<CustomerViewModel>(x => { collection = x; });

			collection.Should().Have.SameSequenceAs(Known.CustomerModel.All);
		}

		[TestMethod]
		public void ServiceFactoryGetCollectionWithoutCompletedActionVerifyServiceIsNotCalled()
		{
			factoryHelper.ServiceFactory
				.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0));

			factoryHelper.CollectionLoaded.Should().Be.False();
		}
	}
}