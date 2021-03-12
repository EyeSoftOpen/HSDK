namespace EyeSoft.Core.Test.Reflection
{
    using System.Linq;
    using EyeSoft.Reflection;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ReflectorTest
	{
		[TestMethod]
		public void VerifyPropertyNameByExpression()
		{
			Reflector.PropertyName<Customer>(x => x.MainAddress)
				.Should().Be("MainAddress", "The property name extracted from the expression is wrong.");
		}

		[TestMethod]
		public void VerifyPropertyByExpression()
		{
			Reflector.Property<Customer>(x => x.MainAddress)
				.Name.Should().Be("MainAddress", "The property name extracted from the expression is wrong.");
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfo()
		{
			var methodInfo = Reflector.GetMethod<Foo>(c => c.AMethod());
			methodInfo.Name.Should().Be("AMethod");
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfoForGenericMethod()
		{
			var methodInfo = Reflector.GetMethod<Foo>(c => c.AMethod(default(int)));

			methodInfo.Name.Should().Be("AMethod");
			methodInfo.GetParameters().First().ParameterType.Should().Be(typeof(int));
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfoForGenericParameterMethod()
		{
			var methodInfo = Reflector.GetMethod<Foo>(c => c.AMethod<int>(default(int)));

			methodInfo.Name.Should().Be("AMethod");
			methodInfo.GetParameters().First().ParameterType.Should().Be(typeof(int));
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfoForStaticMethodOnStaticClass()
		{
			var methodInfo = Reflector.GetMethod(() => StaticFoo.StaticMethod());

			methodInfo.Name.Should().Be("StaticMethod");
			methodInfo.IsStatic.Should().BeTrue();
		}

		private static class StaticFoo
		{
			public static void StaticMethod()
			{
			}
		}

		private abstract class Foo
		{
			public void AMethod()
			{
			}

			public void AMethod(int i)
			{
			}

			public void AMethod<T>(T parameter)
			{
			}
		}
	}
}