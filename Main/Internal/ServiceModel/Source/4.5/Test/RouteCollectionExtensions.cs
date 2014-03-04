namespace EyeSoft.ServiceModel.Test
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Moq;

	using SharpTestsEx;

	[TestClass]
	public class RouteCollectionExtensionsTest
	{
		private interface IFooService
		{
		}

		[TestMethod]
		public void RegisterServiceWithDefaultBindings()
		{
			Executing.This(ConfigureService).Should().NotThrow();
		}

		private static void ConfigureService()
		{
			var serviceHostConfigurator = new ServiceHostConfigurator(typeof(IFooService), null, null);
			var serviceHostFactory = new LocatorServiceHostFactory(serviceHostConfigurator, new Mock<ILocator>().Object);
			serviceHostFactory.Should().Not.Be.Null();
		}
	}
}