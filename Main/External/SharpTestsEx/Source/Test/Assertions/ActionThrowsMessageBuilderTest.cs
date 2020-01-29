using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class ActionThrowsMessageBuilderTest
	{
		[Test]
		public void WhenActualIsNull_ThenContainTheActionAndTheExpected()
		{
			var mb = new ActionThrowsMessageBuilder();
			var message = mb.Compose(new MessageBuilderInfo<Type, Type> { Actual = null, Expected = typeof(ArgumentNullException)});
			message.Should().Not.Contain("null").And.EndWith("System.ArgumentNullException.");
		}

		[Test]
		public void WhenActualIsNotNull_ThenContainTheActionTheExpectedAndAndTheActual()
		{
			var mb = new ActionThrowsMessageBuilder();
			var message = mb.Compose(new MessageBuilderInfo<Type, Type> { Actual = typeof(ArgumentNullException), Expected = typeof(Exception) });
			message.Should().Contain("System.ArgumentNullException").And.Contain("System.Exception");
		}
	}
}