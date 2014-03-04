namespace EyeSoft.ServiceModel
{
	using System;
	using System.ServiceModel;

	public static class ChannelFactoryExtensions
	{
		public static TResult Get<TContract, TResult>(this ChannelFactory<TContract> factory, Func<TContract, TResult> func)
			where TContract : class
		{
			using (factory)
			{
				try
				{
					var proxy = factory.CreateChannel();

					var result = func(proxy);

					factory.Close();

					return result;
				}
				catch
				{
					factory.Abort();
					throw;
				}
			}
		}

		public static void Execute<TContract>(this ChannelFactory<TContract> factory, Action<TContract> action) where TContract : class
		{
			using (factory)
			{
				try
				{
					var proxy = factory.CreateChannel();

					action(proxy);

					factory.Close();
				}
				catch
				{
					factory.Abort();
					throw;
				}
			}
		}
	}
}