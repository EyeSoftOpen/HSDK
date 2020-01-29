using System;

namespace SharpTestsEx
{
	public interface IExpressionActionConstraints<T>: IConstraints<T>
	{
		/// <summary>
		/// Verifies that the <see cref="Action{T}"/> throws a specific <see cref="Exception"/>.
		/// </summary>
		/// <typeparam name="TException">The specific <see cref="Exception"/> subclass expected. </typeparam>
		/// <returns>Chainable And constraint</returns>
		IActionAndConstraints<IThrowConstraints<TException>, TException> Throws<TException>() where TException : Exception;

		/// <summary>
		/// Verifies that the <see cref="Action"/> throws an <see cref="Exception"/>.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		IActionAndConstraints<IThrowConstraints<Exception>, Exception> Throws();

		/// <summary>
		/// Verifies that the <see cref="Action"/> does not throw any <see cref="Exception"/>.
		/// </summary>
		void NotThrows();
	}
}