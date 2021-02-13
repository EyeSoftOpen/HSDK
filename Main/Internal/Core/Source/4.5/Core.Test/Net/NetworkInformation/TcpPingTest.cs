namespace EyeSoft.Core.Test.Net.NetworkInformation
{
    using System;
    using System.Net.Sockets;
    using EyeSoft.Net.NetworkInformation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

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
			Executing.This(() => TcpPing.Send(CorrectHost)).Should().NotThrow();
		}

		[TestMethod]
		public void TryPingWrongHostFalseOnError()
		{
			TcpPing.Send(WrongDomain).Should().Be.False();
		}

		[TestMethod]
		public void TryPingExistingHostExceptionOnError()
		{
			try
			{
				TcpPing.Send(CorrectHost, Port, ThrowOnError).Should().Be.True();
			}
			catch (Exception ex)
			{
				var socketException = ex as SocketException;
				socketException.Should().Not.Be.Null();
				socketException.ErrorCode.Should().Be(11001);
			}
		}

		[TestMethod]
		public void TryPingWrongHostExceptionOnError()
		{
			Executing.This(() => TcpPing.Send(WrongDomain, Port, ThrowOnError))
				.Should().Throw<SocketException>()
				.And.Exception.ErrorCode.Should()
				.Be.EqualTo(11001);
		}
	}
}