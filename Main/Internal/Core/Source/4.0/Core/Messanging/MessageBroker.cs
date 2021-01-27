// Copyright © GalaSoft Laurent Bugnion 2009-2012
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>13.4.2009</date>
// <project>GalaSoft.MvvmLight.Messaging</project>
// <web>http://www.galasoft.ch</web>
// <license>
// See license.txt in this project or http://www.galasoft.ch/license_MIT.txt
// </license>
// ****************************************************************************

namespace EyeSoft.Core.Messanging
{
    using System;

    public static class MessageBroker
	{
		private static readonly Singleton<IMessageBroker> singleton =
			new Singleton<IMessageBroker>(() => new LocalMessageBroker());

		public static void Set(Func<IMessageBroker> messageBroker)
		{
			singleton.Set(messageBroker);
		}

		public static void Register<TMessage>(object recipient, Action<TMessage> action, Predicate<TMessage> condition)
		{
			singleton.Instance.Register(recipient, null, action, condition);
		}

		public static void Register<TMessage>(object recipient, Action<TMessage> action)
		{
			singleton.Instance.Register(recipient, null, false, action);
		}

		public static void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action)
		{
			singleton.Instance.Register(recipient, null, receiveDerivedMessagesToo, action);
		}

		public static void Register<TMessage>(object recipient, object token, Action<TMessage> action)
		{
			singleton.Instance.Register(recipient, token, false, action);
		}

		public static void Register<TMessage>(
			object recipient,
			object token,
			bool receiveDerivedMessagesToo,
			Action<TMessage> action)
		{
			singleton.Instance.Register(recipient, token, receiveDerivedMessagesToo, action);
		}

		public static void Send<TMessage>(TMessage message)
		{
			singleton.Instance.Send(message);
		}

		public static void Send<TMessage, TTarget>(TMessage message)
		{
			singleton.Instance.Send<TMessage, TTarget>(message);
		}

		public static void Send<TMessage>(TMessage message, object token)
		{
			singleton.Instance.Send(message, token);
		}

		public static void Unregister(object recipient)
		{
			singleton.Instance.Unregister(recipient);
		}

		public static void Unregister<TMessage>(object recipient)
		{
			singleton.Instance.Unregister<TMessage>(recipient);
		}

		public static void Unregister<TMessage>(object recipient, object token)
		{
			singleton.Instance.Unregister<TMessage>(recipient, token);
		}

		public static void Unregister<TMessage>(object recipient, Action<TMessage> action)
		{
			singleton.Instance.Unregister(recipient, action);
		}

		public static void Unregister<TMessage>(object recipient, object token, Action<TMessage> action)
		{
			singleton.Instance.Unregister(recipient, token, action);
		}
	}
}