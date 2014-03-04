namespace EyeSoft.Test.Reflection
{
	using System.Linq;

	using EyeSoft.Reflection;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ReflectorTest
	{
		[TestMethod]
		public void VerifyPropertyNameByExpression()
		{
			Reflector.PropertyName<Customer>(x => x.MainAddress)
				.Should("The property name extracted from the expression is wrong.").Be.EqualTo("MainAddress");
		}

		[TestMethod]
		public void VerifyPropertyByExpression()
		{
			Reflector.Property<Customer>(x => x.MainAddress)
				.Name.Should("The property name extracted from the expression is wrong.").Be.EqualTo("MainAddress");
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfo()
		{
			var methodInfo = Reflector.GetMethod<Foo>(c => c.AMethod());
			methodInfo.Name.Should().Be.EqualTo("AMethod");
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfoForGenericMethod()
		{
			var methodInfo = Reflector.GetMethod<Foo>(c => c.AMethod(default(int)));

			methodInfo.Name.Should().Be.EqualTo("AMethod");
			methodInfo.GetParameters().First().ParameterType.Should().Be.EqualTo(typeof(int));
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfoForGenericParameterMethod()
		{
			var methodInfo = Reflector.GetMethod<Foo>(c => c.AMethod<int>(default(int)));

			methodInfo.Name.Should().Be.EqualTo("AMethod");
			methodInfo.GetParameters().First().ParameterType.Should().Be.EqualTo(typeof(int));
		}

		[TestMethod]
		public void GetMethodInfoShouldReturnMethodInfoForStaticMethodOnStaticClass()
		{
			var methodInfo = Reflector.GetMethod(() => StaticFoo.StaticMethod());

			methodInfo.Name.Should().Be.EqualTo("StaticMethod");
			methodInfo.IsStatic.Should().Be.True();
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