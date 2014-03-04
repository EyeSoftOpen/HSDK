namespace EyeSoft.Testing.Test.ServiceModel
{
	using System;
	using System.ServiceModel;

	using Castle.DynamicProxy.Generators;

	using EyeSoft.Testing.ServiceModel;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Moq;

	using SharpTestsEx;

	[TestClass]
	public class WcfTestHostFactoryTest
	{
		private const string Name = "Jim";

		[ServiceContract]
		public interface IService
		{
			[OperationContract]
			string Hello(string name);
		}

		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			AttributesToAvoidReplicating.Add(typeof(ServiceContractAttribute));
		}

		[TestMethod]
		public void VerifyWcfHostCreateAValidProxyImplementation()
		{
			var mock = new Mock<IService>();
			const string Expected = "Hello Jim";
			mock.Setup(x => x.Hello(Name)).Returns(Expected);

			using (var host = WcfTestHostFactory.Create(mock.Object))
			{
				host
					.CreateProxy()
					.Hello(Name).Should().Be.EqualTo(Expected);
			}
		}

		[TestMethod]
		public void VerifyWcfHostCreateAValidProxyFaultImplementationThrowExpectedException()
		{
			var mock = new Mock<IService>();
			mock.Setup(x => x.Hello(It.IsAny<string>())).Throws<ArgumentException>();

			using (var host = WcfTestHostFactory.Create(mock.Object))
			{
				Executing
					.This(() => host.CreateProxy().Hello(Name))
					.Should().Throw<FaultException<ExceptionDetail>>()
					.And
					.Exception.Detail.Type.Should().Be.EqualTo(typeof(ArgumentException).FullName);
			}
		}
	}
}