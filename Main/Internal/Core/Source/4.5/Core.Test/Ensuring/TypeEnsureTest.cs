namespace EyeSoft.Test.Ensuring
{
	using System;
	using System.Collections.ObjectModel;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeEnsureTest
	{
		[TestMethod]
		public void EnsureNotEnumerableTypeIsNotEnumerable()
		{
			Action ensure = () =>
				Ensure
					.That(typeof(string))
					.Is.Enumerable();

			this
				.Executing(a => ensure())
				.Throws<TypeIsNotEnumerableException>();
		}

		[TestMethod]
		public void EnsureEnumerableTypeIsEnumerable()
		{
			Action ensure = () =>
				Ensure
					.That(typeof(ReadOnlyCollection<>))
					.Is.Enumerable();

			this
				.Executing(a => ensure())
				.NotThrows();
		}
	}
}