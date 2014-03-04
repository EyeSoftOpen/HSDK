namespace EyeSoft.Test.IO
{
	using EyeSoft.IO;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class PathExtensionsTest
	{
		[TestMethod]
		public void VerifyTrimmedPathDoesNotContainDirectorySeparator()
		{
			@"C:\temp\".TrimPath().Should().Be.EqualTo("c:\temp");
		}

		[TestMethod]
		public void CompareEqualPaths()
		{
			PathExtensions.Equals(@"c:\temp", @"c:\Temp\").Should().Be.True();
		}

		[TestMethod]
		public void CompareDifferentPaths()
		{
			PathExtensions.Equals(@"c:\temp1", @"c:\Temp").Should().Be.False();
		}
	}
}