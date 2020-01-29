using System;

namespace SharpTestsEx
{
	/// <summary>
	/// Useful class to avoid the creation of new Action.
	/// </summary>
	/// <remarks>
	/// This class can be used when the instance of the class under test is no available;
	/// typically to test a constructor.
	/// When you have an instance of the class under test the most appropite way to test an action
	/// is the extension <see cref="Extensions.Executing{T}"/>.
	/// </remarks>
	/// <example>
	/// <code>
	/// Executing.This(() => new AClass(null)).Should().Throw();
	/// </code>
	/// <code>
	/// Executing.This(() => new AClass(null)).Should().Throw{ArgumentNullException}()
	///			.And.ValueOf
	///				.ParamName.Should().Be("obj");
	/// </code>
	/// </example>
	public static class Executing
	{
		public static Action This(Action action)
		{
			return action;
		}
	}
}