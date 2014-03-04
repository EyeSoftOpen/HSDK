namespace EyeSoft.Testing.ServiceModel
{
	public static class WcfTestHostFactory
	{
		public static WcfTestHost<TServiceContract> Create<TServiceContract, TService>()
			where TService : class, TServiceContract, new()
		{
			return
				new WcfTestHost<TServiceContract>(new TService(), NetTcpServiceConfigurationFactory.Create(), false);
		}

		public static WcfTestHost<TServiceContract> Create<TServiceContract>(TServiceContract service)
		{
			return
				new WcfTestHost<TServiceContract>(service, NetTcpServiceConfigurationFactory.Create(), false);
		}

		public static WcfTestHost<TServiceContract> CreateWithErrorHandler<TServiceContract, TService>()
			where TService : class, TServiceContract, new()
		{
			return
				new WcfTestHost<TServiceContract>(new TService(), NetTcpServiceConfigurationFactory.Create(), true);
		}

		public static WcfTestHost<TServiceContract> CreateWithErrorHandler<TServiceContract>(TServiceContract service)
		{
			return
				new WcfTestHost<TServiceContract>(service, NetTcpServiceConfigurationFactory.Create(), true);
		}
	}
}