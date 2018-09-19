namespace EyeSoft.Test.Ensuring
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class EnsureEnumerableTest
	{
		[TestMethod]
		public void EnforceEnumerableNotEmpty()
		{
			Action ensure =
				() =>
					Ensure
						.That(new List<string>().AsEnumerable())
						.Is.Not.Empty();

			Executing
				.This(ensure)
				.Should()
				.Throw<CollectionIsEmptyException>();
		}

		[TestMethod]
		public void EnforceEnumerableNotContaining()
		{
			const int Element = 1;

			var enumerable = new[] { Element }.AsEnumerable();

			Action ensure =
				() =>
					Ensure.That(enumerable)
					.Is.Not.Containing(Element);

			Executing
				.This(ensure)
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void EnforceEnumerableContaining()
		{
			const int Element = 1;

			var enumerable = new[] { Element }.AsEnumerable();

			Action ensure =
				() =>
					Ensure.That(enumerable)
					.Is.Containing(Element);

			Executing
				.This(ensure)
				.Should().NotThrow();
		}
	}
}