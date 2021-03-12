namespace EyeSoft.Core.Test.Reflection
{
    using System.Reflection;
    using EyeSoft.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class AssemblyExtensionsTest
	{
		[TestMethod]
		public void VerifyAssemblyProduct()
		{
			Assembly.GetAssembly(typeof(AssemblyExtensionsTest)).Product().Should().Be("EyeSoft.Core");
		}

		[TestMethod]
		public void VerifyAssemblyCompany()
		{
			Assembly.GetAssembly(typeof(AssemblyExtensionsTest)).Company().Should().Be("EyeSoft");
		}
	}
}