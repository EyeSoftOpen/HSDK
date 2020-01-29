using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class ExpressionMessageComposerTest
	{
		private class MagnifierStub: IFailureMagnifier
		{
			private readonly string message;

			public MagnifierStub(string message)
			{
				this.message = message;
			}

			#region Implementation of IFailureMagnifier

			public string Message()
			{
				return message;
			}

			#endregion
		}

		[Test]
		public void WhenCalledWithNull_ShouldThrows()
		{
			Executing.This(() => new ExpressionMessageComposer<object>(null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void WhenCalledWithNotNull_ShouldNotThrows()
		{
			Executing.This(() => new ExpressionMessageComposer<object>(a=> a != null)).Should().NotThrow();
		}

		[Test]
		public void MessageShouldContainExpression()
		{
			var ex = new ExpressionMessageComposer<object>(a => a != null);
			ex.GetMessage(null, null).Should().Contain("a => a != (null)");
		}

		[Test]
		public void MessageShouldContainActual()
		{
			var ex = new ExpressionMessageComposer<int>(a => a > 0);
			var message = ex.GetMessage(101, null);
			message.Should().Contain("101");
			message.IndexOf("101").Should().Be.LessThan(message.IndexOf("Satisfy"));
		}

		[Test]
		public void MessageShouldContainCustomMessage()
		{
			var ex = new ExpressionMessageComposer<object>(a => a != null);
			ex.GetMessage(null, "My Custom message").Should().Contain("My Custom message");
		}

		[Test]
		public void WhenHasMagnifier_TheMessageContainTheMagnifierMessageToo()
		{
			var magnifier = new MagnifierStub("pizza");
			var ex = new ExpressionMessageComposer<object>(a => a != null, magnifier);
			ex.GetMessage(null, null).Should().Contain("pizza");
		}
	}
}