namespace EyeSoft.Core.Test
{
    using EyeSoft.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ComparerFactoryTest
	{
		[TestMethod]
		public void CreateComparerFromExpression()
		{
			var comparer = ComparerFactory<Person>.Create(x => x.Age);

			comparer.Compare(new Person { Age = 1 }, new Person { Age = 1 })
				.Should().Be(0);
		}

		private class Person
		{
			public int Age { get; set; }
		}
	}
}