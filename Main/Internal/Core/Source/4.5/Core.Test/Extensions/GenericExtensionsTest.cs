namespace EyeSoft.Core.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class GenericExtensionsTest
	{
		private interface IAggregate
		{
		}

		[TestMethod]
		public void IsDefaultOnGenericDefaultValueReturnsTrue()
		{
			default(int).IsDefault().Should().Be.True();
		}

		[TestMethod]
		public void IsDefaultOnGenericDefaultValueUsingComparableReturnsTrue()
		{
			default(Guid).IsDefault<IComparable>().Should().Be.True();
		}

		[TestMethod]
		public void IsDefaultOnIntDefaultValueUsingComparableReturnsTrue()
		{
			default(int).IsDefault<IComparable>().Should().Be.True();
		}

		[TestMethod]
		public void IsDefaultOnObjectDefaultValueUsingComparableReturnsTrue()
		{
			default(object).IsDefault().Should().Be.True();
		}

		[TestMethod]
		public void GetOneObjectEnumerableFromASingleInstance()
		{
			new Customer("Bill", "Olmi Street")
				.Extend().GetEnumerable()
				.Count().Should().Be.EqualTo(1);
		}

		[TestMethod]
		public void GetOneObjectEnumerableFromAList()
		{
			new List<Customer> { new Customer("Bill", "Olmi Street"), new Customer("Steve", "Oleandri Street") }
				.Extend().GetEnumerable()
				.Count().Should().Be.EqualTo(2);
		}

		[TestMethod]
		public void GetOneObjectEnumerableFromAReadOnlyList()
		{
			new List<Customer> { new Customer("Bill", "Olmi Street"), new Customer("Steve", "Oleandri Street") }
				.AsReadOnly()
				.Extend().GetEnumerable()
				.Count().Should().Be.EqualTo(2);
		}

		[TestMethod]
		public void FlatternCollectionOfChildrenFromAnInstance()
		{
			Known.Schools
				.SchoolWithTwoChildren
				.Extend().GetFlatternChildren(type => type.IsEnumerableOf<IAggregate>() || type.Implements<IAggregate>())
				.Cast<IAggregate>()
				.Count().Should().Be.EqualTo(2);
		}

		private static class Known
		{
			public static class Schools
			{
				public static class SchoolWithTwoChildren
				{
					public static IObjectExtender<School> Extend()
					{
						return new ObjectExtender<School>(new School("Bill", "Steve"));
					}
				}
			}
		}

		private class School
		{
			public School(string bill, string steve)
			{
				Children = new[] { new Child(bill), new Child(steve) };
			}

			private IEnumerable<IAggregate> Children { get; }
		}

		private class Child : IAggregate
		{
			public Child(string name)
			{
				Name = name;
			}

			private string Name { get; }
		}
	}
}