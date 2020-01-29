using System;
namespace SharpTestsEx
{
	/// <summary>
	/// Constraints for <see cref="Action"/>.
	/// </summary>
	public interface IActionConstraints : IConstraints<Action>
	{
		/// <summary>
		/// Verifies that the <see cref="Action"/> throws a specific <see cref="Exception"/>.
		/// </summary>
		/// <typeparam name="TException">The specific <see cref="Exception"/> subclass expected. </typeparam>
		/// <returns>Chainable And constraint</returns>
		IActionAndConstraints<IThrowConstraints<TException>, TException> Throw<TException>() where TException : Exception;

		/// <summary>
		/// Verifies that the <see cref="Action"/> throws an.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		IActionAndConstraints<IThrowConstraints<Exception>, Exception> Throw();

		/// <summary>
		/// Verifies that the <see cref="Action"/> does not throw any <see cref="Exception"/>.
		/// </summary>
		void NotThrow();
	}

	public interface IActionAndConstraints<TConstraints, TException> : IAndConstraints<TConstraints>
		where TConstraints : class, IAllowClone
		where TException : Exception
	{
		/// <summary>
		/// The instance <see cref="Exception"/> thrown.
		/// </summary>
		/// <remarks>
		/// <example>
		/// <code>
		/// var ex = (new Action(() => new AClass(null))).Should().Throw().Exception;
		/// </code>
		/// </example>
		/// </remarks>
		TException Exception { get; }
	}

	/// <summary>
	/// Chainable constraint for <see cref="Exception"/>
	/// </summary>
	/// <typeparam name="TException">The specific <see cref="Exception"/> subclass expected. </typeparam>
	public interface IThrowConstraints<TException> : IAllowClone where TException : Exception
	{
		/// <summary>
		/// The <see cref="Exception"/> thrown.
		/// </summary>
		/// <remarks>
		/// Allow an readable chained way to begin a new assertion based on one of the properties
		/// of the expected <see cref="Exception"/>
		/// <example>
		/// <code>
		/// (new Action(() => new AClass(null)))
		///			.Should().Throw{ArgumentNullException}()
		///			.And.ValueOf.ParamName
		///					.Should().Be.EqualTo("obj");
		/// </code>
		/// </example>
		/// </remarks>
		TException ValueOf { get; }

		/// <summary>
		/// The instance <see cref="Exception"/> thrown.
		/// </summary>
		/// <remarks>
		/// Allow an readable chained way to begin a new assertion based on the <see cref="Exception"/> itself.
		/// <example>
		/// <code>
		/// (new Action(() => new AClass(null)))
		///			.Should().Throw()
		///			.And.Exception.Should().Be.InstanceOf{ArgumentException}();
		/// </code>
		/// </example>
		/// </remarks>
		TException Exception { get; }
	}
}