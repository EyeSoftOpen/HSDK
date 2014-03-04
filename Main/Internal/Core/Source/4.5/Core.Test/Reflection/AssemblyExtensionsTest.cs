namespace EyeSoft.Test.Reflection
{
	using System.Reflection;

	using EyeSoft.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class AssemblyExtensionsTest
	{
		[TestMethod]
		public void VerifyAssemblyProduct()
		{
			Assembly.GetAssembly(typeof(AssemblyExtensionsTest)).Product().Should().Be.EqualTo("EyeSoft.Core");
		}

		[TestMethod]
		public void VerifyAssemblyCompany()
		{
			Assembly.GetAssembly(typeof(AssemblyExtensionsTest)).Company().Should().Be.EqualTo("EyeSoft");
		}
	}
}