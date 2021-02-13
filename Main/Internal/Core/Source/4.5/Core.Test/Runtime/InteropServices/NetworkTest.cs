namespace EyeSoft.Core.Test.Runtime.InteropServices
{
    using EyeSoft.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SharpTestsEx;

    [TestClass]
	public class NetworkTest
	{
		[TestMethod]
		public void VerifyInternetConnectionCheckWithCustomerCheckerReturnsTrue()
		{
			CheckInternetConnection(true);
		}

		[TestMethod]
		public void VerifyInternetConnectionCheckWithCustomerCheckerReturnsFalse()
		{
			CheckInternetConnection(false);
		}

		private static void CheckInternetConnection(bool connectionAvailable)
		{
			var networkCheckerMock = new Mock<INetworkChecker>();
			networkCheckerMock.SetupGet(x => x.IsInternetAvailable).Returns(connectionAvailable);

			Network.Set(networkCheckerMock.Object);
			Network.IsInternetAvailable.Should().Be.EqualTo(connectionAvailable);
		}
	}
}
