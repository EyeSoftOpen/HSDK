using System;
using NUnit.Framework;

namespace SharpTestsEx.Tests
{
	
	public class AssertExceptionTest
	{
		[Test]
		public void StackTraceFiltered()
		{
			var e = Executing.This(() => "foo".Should().Be.EqualTo("bar")).Should().Throw().And.Exception;
			e.StackTrace.Should().Not.Contain("StringConstraints");
			e.StackTrace.Lines().Length.Should("->" + e.StackTrace).Be.EqualTo(2);
		}
	}
}