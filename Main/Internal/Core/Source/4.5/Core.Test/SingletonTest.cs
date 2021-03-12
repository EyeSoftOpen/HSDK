namespace EyeSoft.Core.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class SingletonTest
	{
		[TestMethod]
		public void VerifyASingletonInstanceNotInitializedThrowTheCorrectException()
		{
			var singleton = new Singleton<Service>();

            Action action = () => singleton.Instance.Execute();

            action.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void VerifyAssignANullReferenceToASingletonInstanceThrowAnArgumentNullException()
		{
			var singleton = new Singleton<Service>();

            Action action = () => singleton.Set((Service)null);
            action.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void VerifyASingletonInstanceCanBeAssignedOnlyOneTime()
		{
			var singleton = new Singleton<Service>(() => new Service());

			singleton.Set(() => new Service());

            Action action = () => singleton.Set(() => new Service());
                
            action.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void VerifyASingletonInstanceCannotBeChangedAfterTheFirstUsage()
		{
			var singleton = new Singleton<Service>();
			singleton.Set(() => new Service());

			singleton.Instance.Execute();

            Action action = () => singleton.Set(() => new Service());
            action.Should().Throw<InvalidOperationException>();
		}

		private class Service
		{
			public void Execute()
			{
			}
		}
	}
}