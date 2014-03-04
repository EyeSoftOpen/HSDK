namespace EyeSoft.Test.Messanging
{
	using EyeSoft.Messanging;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class MessageBrokerTest
	{
		private const string MessageTitle = "Title1";
		private const string MessageTitle2 = "Title2";

		[TestMethod]
		public void SendMessageAndCheckIsSent()
		{
			var messageReceived = false;

			MessageBroker.Register<Message>(
				this,
				a =>
					{
						messageReceived = true;
						Assert.AreEqual(MessageTitle, a.Title);
					});

			MessageBroker.Send(new Message { Title = MessageTitle });

			messageReceived
				.Should()
				.Be
				.True();
		}

		[TestMethod]
		public void SendMessageWithVerifiedConditionOnReceiveAndCheckIsSent()
		{
			SendMessageAndCheckTheReceiver(MessageTitle, true);
		}

		[TestMethod]
		public void SendMessageWithNotVerifiedConditionOnReceiveAndCheckIsSent()
		{
			SendMessageAndCheckTheReceiver(MessageTitle2, false);
		}

		private void SendMessageAndCheckTheReceiver(string conditionalMessageTitle, bool expected)
		{
			var messageReceived = false;

			MessageBroker.Register<Message>(
				this,
				message =>
					{
						messageReceived = true;
						message.Title.Should().Be.EqualTo(MessageTitle);
					},
				message => message.Title == conditionalMessageTitle);

			MessageBroker.Send(new Message { Title = MessageTitle });

			messageReceived
				.Should()
				.Be
				.EqualTo(expected);
		}
	}
}