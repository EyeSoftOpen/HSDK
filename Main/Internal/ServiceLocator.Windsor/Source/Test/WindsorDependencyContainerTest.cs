namespace EyeSoft.ServiceLocator.Windsor.Test
{
	using EyeSoft.ServiceLocator.Test;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class WindsorDependencyContainerTest : DependencyContainerTest
	{
		[TestInitialize]
		public override void Inizialize()
		{
			base.Inizialize();
		}

		[TestMethod]
		public override void ResolveAServiceWithRuntimeParameters()
		{
			base.ResolveAServiceWithRuntimeParameters();
		}

		[TestMethod]
		public override void ResolveANotRegisterServiceExpectedNullInstance()
		{
			base.ResolveANotRegisterServiceExpectedNullInstance();
		}

		protected override IDependencyContainer CreateDependencyContainer()
		{
			var windsorContainer = new WindsorDependencyContainer();

			IDependencyContainer container = windsorContainer;

			return container;
		}
	}
}