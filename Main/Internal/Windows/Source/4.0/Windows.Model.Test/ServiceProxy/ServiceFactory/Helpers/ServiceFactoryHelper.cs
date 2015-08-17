namespace EyeSoft.Windows.Model.Test
{
	using System;

	using EyeSoft.AutoMapper;
	using EyeSoft.Mapping;
	using EyeSoft.Threading.Tasks;
	using EyeSoft.Windows.Model.Demo.Configuration;
	using EyeSoft.Windows.Model.Demo.Configuration.Helpers;
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers;

	public class ServiceFactoryHelper : IDisposable
	{
		private readonly CustomerServiceStub service;

		static ServiceFactoryHelper()
		{
			ThreadingFactory.SetCurrentThreadScheduler();

			Mapper.Set(() => new AutoMapperMapper());
		}

		private ServiceFactoryHelper(ServiceFactory<ICustomerService> serviceFactory, CustomerServiceStub service)
		{
			ServiceFactory = serviceFactory;
			this.service = service;
		}

		public ServiceFactory<ICustomerService> ServiceFactory { get; private set; }

		public bool ItemLoaded => service.ItemLoaded;

		public bool CollectionLoaded => service.CollectionLoaded;

		public static ServiceFactoryHelper Create()
		{
			var serviceStub = new CustomerServiceStub();

			var customerServiceFactory = new DemoProxyFactory<ICustomerService>(serviceStub);

			var serviceFactory = new ServiceFactory<ICustomerService>(() => customerServiceFactory.Create());

			var factory = new ServiceFactoryHelper(serviceFactory, serviceStub);

			return factory;
		}

		public void Dispose()
		{
		}
	}
}