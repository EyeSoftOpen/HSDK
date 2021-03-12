namespace EyeSoft.Windows.Model.Test.ServiceProxy.ServiceFactory.Collection.Direct
{
    using System.Threading;
    using Demo.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Collections.ObjectModel;
    using FluentAssertions;

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

			collection.Should().BeSameAs(Demo.Configuration.Helpers.Known.CustomerModel.All);
		}

		[TestMethod]
		public void ServiceFactoryGetCollectionWithoutCompletedActionVerifyServiceIsNotCalled()
		{
			factoryHelper.ServiceFactory
				.Fill(x => x.GetCustomersWithTurnoverGreatherThan(0));

			factoryHelper.CollectionLoaded.Should().BeFalse();
		}
	}
}