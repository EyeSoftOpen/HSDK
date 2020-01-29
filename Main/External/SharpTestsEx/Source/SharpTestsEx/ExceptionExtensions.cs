using System;
using System.Collections.Generic;

namespace SharpTestsEx
{
	/// <summary>
	/// Useful extensions to test <see cref="Exception"/>s.
	/// </summary>
	public static class ExceptionExtensions
	{
		/// <summary>
		/// Returns a sequence of all Inner Exceptions.
		/// </summary>
		/// <param name="source">The root <see cref="Exception"/> </param>
		/// <returns>A <see cref="IEnumerable{Exception}"/> of all Inner Exceptions</returns>
		public static IEnumerable<Exception> InnerExceptions(this Exception source)
		{
			Exception current = source.InnerException;
			while (current != null)
			{
				yield return current;
				current = current.InnerException;
			}
		}

		/// <summary>
		/// Returns a sequence of including the root <see cref="Exception"/> and all Inner Exceptions.
		/// </summary>
		/// <param name="source">The root <see cref="Exception"/> </param>
		/// <returns>A <see cref="IEnumerable{Exception}"/> of including the root <see cref="Exception"/> and all Inner Exceptions.</returns>
		public static IEnumerable<Exception> Exceptions(this Exception source)
		{
			Exception current = source;
			while (current != null)
			{
				yield return current;
				current = current.InnerException;
			}
		}
	}
}