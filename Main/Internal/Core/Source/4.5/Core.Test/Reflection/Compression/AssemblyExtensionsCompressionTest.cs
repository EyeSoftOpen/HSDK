namespace EyeSoft.Test.Reflection.Compression
{
	using System.Text;

	using EyeSoft.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class AssemblyExtensionsCompressionTest
	{
		[TestMethod]
		public void ReadGzipCompressedResource()
		{
			var contents = "This is a compression test. With accent è." + new string('c', 100);

			GetType().Assembly.ReadGzipResourceText("EyeSoft.Core.Test.Reflection.Compression.Source.txt.gz", true, Encoding.UTF8).Should().Be.EqualTo("This is a compression test. With accent �.cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc");
		}
	}
}
