namespace EyeSoft.Windows.Model.Test.Collections.ObjectModel
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Collections;
    using FluentAssertions;

    [TestClass]
	public class FullTextFilterTest
	{
		[TestMethod]
		public void FullNameFilterNullThrowsException()
		{
			Action action = () => new FullTextFilter<Subject>(Known.Subjects.ByName, null).HasToInclude();

			action.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void FullNameFilterWithNotContainedKeywordReturnFalse()
		{
			new FullTextFilter<Subject>(new Subject { FirstName = "Bill", LastName = "Gates" }, "w")
				.HasToInclude().Should().BeFalse();
		}

		[TestMethod]
		public void FullNameFilterWithMultiplePartialContainedKeywordsReturnTrue()
		{
			new FullTextFilter<Subject>(new Subject { FirstName = "Bill", LastName = "Gates" }, "b", "g")
				.HasToInclude().Should().BeTrue();
		}

		[TestMethod]
		public void ContainedLowerCaseFullNameReturnTrue()
		{
			new FullTextFilter<Subject>(Known.Subjects.ByName, "b")
				.HasToInclude().Should().BeTrue();
		}

		[TestMethod]
		public void ContainedUpperCaseFullNameReturnFalse()
		{
			new FullTextFilter<Subject>(Known.Subjects.ByName, "B")
				.HasToInclude().Should().BeFalse();
		}
	}
}