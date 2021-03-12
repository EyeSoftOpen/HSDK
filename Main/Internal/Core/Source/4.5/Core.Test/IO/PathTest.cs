namespace EyeSoft.Core.Test.IO
{
    using EyeSoft.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class PathExtensionsTest
	{
		[TestMethod]
		public void VerifyTrimmedPathDoesNotContainDirectorySeparator()
		{
			@"c:\temp\".TrimPath().Should().Be(@"c:\temp");
		}

		[TestMethod]
		public void CompareEqualPaths()
		{
			PathExtensions.Equals(@"c:\temp", @"c:\Temp\").Should().BeTrue();
		}

		[TestMethod]
		public void CompareDifferentPaths()
		{
			PathExtensions.Equals(@"c:\temp1", @"c:\Temp").Should().BeFalse();
		}
	}
}