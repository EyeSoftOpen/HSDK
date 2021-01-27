namespace EyeSoft.Core.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class SingletonTest
	{
		[TestMethod]
		public void VerifyASingletonInstanceNotInitializedThrowTheCorrectException()
		{
			var singleton = new Singleton<Service>();

			Executing
				.This(() => singleton.Instance.Execute())
				.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void VerifyAssignANullReferenceToASingletonInstanceThrowAnArgumentNullException()
		{
			var singleton = new Singleton<Service>();

			Executing
				.This(() => singleton.Set((Service)null))
				.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void VerifyASingletonInstanceCanBeAssignedOnlyOneTime()
		{
			var singleton = new Singleton<Service>(() => new Service());

			singleton.Set(() => new Service());

			Executing
				.This(() => singleton.Set(() => new Service()))
				.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void VerifyASingletonInstanceCannotBeChangedAfterTheFirstUsage()
		{
			var singleton = new Singleton<Service>();
			singleton.Set(() => new Service());

			singleton.Instance.Execute();

			Executing
				.This(() => singleton.Set(() => new Service()))
				.Should().Throw<InvalidOperationException>();
		}

		private class Service
		{
			public void Execute()
			{
			}
		}
	}
}