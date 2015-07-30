namespace EyeSoft.Windows.Model.Test
{
	using System.Threading;

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

			var manualResetEvent = new ManualResetEvent(false);

			factoryHelper
				.ServiceFactory
				.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0))
				.Completed<CustomerViewModel>(
					x =>
						{
							collection = x;
							
							manualResetEvent.Set();
						});

			manualResetEvent.WaitOne();

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