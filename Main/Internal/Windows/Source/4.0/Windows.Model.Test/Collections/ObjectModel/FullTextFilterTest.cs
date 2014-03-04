namespace EyeSoft.Windows.Model.Test.ObjectModel
{
	using System;

	using EyeSoft.Windows.Model.Collections;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class FullTextFilterTest
	{
		[TestMethod]
		public void FullNameFilterNullThrowsException()
		{
			Executing.This(() => new FullTextFilter<Subject>(Known.Subjects.ByName, null).HasToInclude())
				.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void FullNameFilterWithNotContainedKeywordReturnFalse()
		{
			new FullTextFilter<Subject>(new Subject { FirstName = "Bill", LastName = "Gates" }, "w")
				.HasToInclude().Should().Be.False();
		}

		[TestMethod]
		public void FullNameFilterWithMultiplePartialContainedKeywordsReturnTrue()
		{
			new FullTextFilter<Subject>(new Subject { FirstName = "Bill", LastName = "Gates" }, "b", "g")
				.HasToInclude().Should().Be.True();
		}

		[TestMethod]
		public void ContainedLowerCaseFullNameReturnTrue()
		{
			new FullTextFilter<Subject>(Known.Subjects.ByName, "b")
				.HasToInclude().Should().Be.True();
		}

		[TestMethod]
		public void ContainedUpperCaseFullNameReturnFalse()
		{
			new FullTextFilter<Subject>(Known.Subjects.ByName, "B")
				.HasToInclude().Should().Be.False();
		}
	}
}