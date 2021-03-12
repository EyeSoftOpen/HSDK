namespace EyeSoft.Core.Test.Net.NetworkInformation
{
    using System;
    using System.Net.Sockets;
    using EyeSoft.Net.NetworkInformation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class TcpPingTest
	{
		private const string CorrectHost = "www.microsoft.com";
		private const string WrongDomain = "www.microsoft.com2";

		private const int Port = 80;

		private const bool ThrowOnError = true;

		[TestMethod]
		public void TryPingExistingHostFalseOnError()
		{
			Action action = () => TcpPing.Send(CorrectHost);
            action.Should().NotThrow();
		}

		[TestMethod]
		public void TryPingWrongHostFalseOnError()
		{
			TcpPing.Send(WrongDomain).Should().BeFalse();
		}

		[TestMethod]
		public void TryPingExistingHostExceptionOnError()
		{
			try
			{
				TcpPing.Send(CorrectHost, Port, ThrowOnError).Should().BeTrue();
			}
			catch (Exception ex)
			{
				var socketException = ex as SocketException;
				socketException.Should().NotBeNull();
				socketException?.ErrorCode.Should().Be(11001);
			}
		}

		[TestMethod]
		public void TryPingWrongHostExceptionOnError()
		{
            Action action = () => TcpPing.Send(WrongDomain, Port, ThrowOnError);
            
            action.Should().Throw<SocketException>().And.ErrorCode.Should().Be(11001);
		}
	}
}