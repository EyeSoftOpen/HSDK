using System;
using System.Linq;
using NUnit.Framework;

namespace SharpTestsEx.Tests
{
	
	public class ExceptionExtensionsTest
	{
		[Test]
		public void InnerExceptionsShouldReturnAllInner()
		{
			var exception = new ArgumentException("mess", new ArgumentNullException("mess", new ArgumentOutOfRangeException()));
			exception.InnerExceptions().Select(e => e.GetType())
				.Should()
				.Have.SameSequenceAs(new[] { typeof(ArgumentNullException), typeof(ArgumentOutOfRangeException) });
		}

		[Test]
		public void ExceptionsShouldReturnAllInnerIncludingExceptionItSelf()
		{
			var exception = new ArgumentException("mess", new ArgumentNullException("mess", new ArgumentOutOfRangeException()));
			exception.Exceptions().Select(e => e.GetType())
				.Should()
				.Have.SameSequenceAs(new[] { typeof(ArgumentException), typeof(ArgumentNullException), typeof(ArgumentOutOfRangeException) });
		}

		public class SillyClass
		{
			public SillyClass(object obj)
			{
				if (obj == null)
				{
					throw new ArgumentException("mess",
						new ArgumentNullException("mess null",
							new ArgumentOutOfRangeException("obj")));
				}
			}
		}

		[Test]
		public void DeepExceptionExamples()
		{
			new Action(() => new SillyClass(null))
				.Should().Throw<ArgumentException>()
				.And.ValueOf.InnerExceptions()
					.OfType<ArgumentOutOfRangeException>().First()
						.ParamName.Should().Be.EqualTo("obj");

			new Action(() => new SillyClass(null))
				.Should().Throw<ArgumentException>()
				.And.ValueOf.InnerExceptions().Select(e => e.GetType())
					.Should().Contain(typeof(ArgumentOutOfRangeException));

			new Action(() => new SillyClass(null))
				.Should().Throw<ArgumentException>()
				.And.ValueOf.InnerException
					.Should().Be.OfType<ArgumentNullException>()
						.And.ValueOf.Message.Should().Be.EqualTo("mess null");
		}

	}
}