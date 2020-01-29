using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class TypeOfAssertionTest
	{
		[Test]
		public void Ctor()
		{
			Executing.This(() => new TypeOfAssertion(null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void ShouldWork()
		{
			var c = new TypeOfAssertion(typeof(int));
			c.Assert(5, null); // should work
		}

		[Test]
		public void ShouldFail()
		{
			var c = new TypeOfAssertion(typeof(int));
			c.Executing(x => x.Assert(1.2, null)).Throws<AssertException>();
		}

		[Test]
		public void ShouldFailWithNull()
		{
			var c = new TypeOfAssertion(typeof(int));
			c.Executing(x => x.Assert(null, null)).Throws<AssertException>();
		}
	}
}