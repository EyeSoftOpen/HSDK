namespace EyeSoft.ServiceLocator.Windsor.Test
{
	using EyeSoft.ServiceLocator.Test;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class WindsorServiceLocatorTest : ServiceLocatorTest
	{
		[TestInitialize]
		public override void Inizialize()
		{
			base.Inizialize();
		}

		[TestMethod]
		public override void AskingForNotRegisteredComponentShouldRaiseComponentResolutionException()
		{
			base.AskingForNotRegisteredComponentShouldRaiseComponentResolutionException();
		}

		[TestMethod]
		public override void GenericOverloadGetAllInstances()
		{
			base.GenericOverloadGetAllInstances();
		}

		[TestMethod]
		public override void GenericOverloadGetInstance()
		{
			base.GenericOverloadGetInstance();
		}

		[TestMethod]
		public override void GenericOverloadGetInstanceWithName()
		{
			base.GenericOverloadGetInstanceWithName();
		}

		[TestMethod]
		public override void GetAllInstances()
		{
			base.GetAllInstances();
		}

		[TestMethod]
		public override void GetInstance()
		{
			base.GetInstance();
		}

		[TestMethod]
		public override void GetlAllInstanceForUnknownTypeReturnEmptyEnumerable()
		{
			base.GetlAllInstanceForUnknownTypeReturnEmptyEnumerable();
		}

		[TestMethod]
		public override void GetNamedInstance()
		{
			base.GetNamedInstance();
		}

		[TestMethod]
		public override void GetNamedInstance2()
		{
			base.GetNamedInstance2();
		}

		[TestMethod]
		public override void GetNamedInstanceWithEmptyName()
		{
			base.GetNamedInstanceWithEmptyName();
		}

		[TestMethod]
		public override void GetUnknownInstance2()
		{
			base.GetUnknownInstance2();
		}

		[TestMethod]
		public override void OverloadGetInstanceNoNameAndNullName()
		{
			base.OverloadGetInstanceNoNameAndNullName();
		}

		protected override ILocator CreateServiceLocator()
		{
			var container = new WindsorDependencyContainer();

			return container;
		}
	}
}