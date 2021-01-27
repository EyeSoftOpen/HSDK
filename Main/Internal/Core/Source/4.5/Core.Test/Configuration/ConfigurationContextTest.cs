namespace EyeSoft.Core.Test.Configuration
{
    using System.Collections.Specialized;
    using Core.Collections.Specialized;
    using Core.Configuration;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SharpTestsEx;

    [TestClass]
	public class ConfigurationContextTest
	{
		[TestMethod]
		public void VerifyExitingKeyInConfiguration()
		{
			var configuration = new Mock<IConfigurationContext>();

			const string Key = "Key1";
			const int ExpectedValue = 903;
			var expectedValueString = ExpectedValue.ToInvariant();

			configuration
				.SetupGet(x => x.AppSettings)
				.Returns(new NameValueCollection { { Key, expectedValueString } });

			ConfigurationContext.Set(configuration.Object);

			ConfigurationContext.AppSettings.Get<int>(Key).Should().Be.EqualTo(ExpectedValue);
		}
	}
}