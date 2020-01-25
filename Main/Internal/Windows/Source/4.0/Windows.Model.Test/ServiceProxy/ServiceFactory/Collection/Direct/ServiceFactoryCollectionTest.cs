namespace EyeSoft.Windows.Model.Test
{
	using System.Threading;

	using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Demo.Configuration.Helpers;
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;
    using EyeSoft.Windows.Model.Test.ObjectModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ServiceFactoryCollectionTest : ServiceFactoryTest
	{
	    public ServiceFactoryCollectionTest()
	    {
	        global::AutoMapper.Mapper.CreateMap<CustomerDto, CustomerViewModel>();
	    }

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

			collection.Should().Have.SameSequenceAs(Demo.Configuration.Helpers.Known.CustomerModel.All);
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